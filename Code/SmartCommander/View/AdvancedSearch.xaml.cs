using SmartCommander.Model;
using SmartCommander.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartCommander.View
{
    /// <summary>
    /// Logique d'interaction pour AdvancedSearch.xaml
    /// </summary>
    public partial class AdvancedSearch : Window
    {
        private List<DirInfo> childFileList;
        private String workdir;

        public AdvancedSearch(String path)
        {
            InitializeComponent();

            workdir = path;

            TBPattern.Text = "";
            TBReplace.Text = "";

            DirInfo CurrentDirectory = new DirInfo(new DirectoryInfo(workdir));
            childFileList = (from fobj in FileSystemExplorerService.GetChildFiles(CurrentDirectory.Path)
                                           select new DirInfo(fobj)).ToList();


            int length = 0;
            if (!int.TryParse(TBLength.Text, out length))
            {
                length = 3;
            }

            Dictionary<String, int> detection = DetectOccurences(childFileList, length);

            LVWord.ItemsSource = detection;

        }

        static public Dictionary<String, int> DetectOccurences(List<DirInfo> namefiles, int lengthMin)
        {
            Dictionary<String, int> detection = new Dictionary<string, int>();
            String nameJoin = String.Join(";", namefiles.Select(x => x.Name).ToArray<String>());
            String pattern = @"\w*?(\w{" + lengthMin + @",}).*\1";

            Regex r = new Regex(pattern);
            MatchCollection mc;
            do
            {
                mc = r.Matches(nameJoin);

                int i = 0;
                foreach (Match m in mc)
                {
                    int nbOcc = Regex.Matches(nameJoin, m.Groups[1].Value).Count;
                    detection.Add(m.Groups[1].Value, nbOcc);
                    nameJoin = nameJoin.Replace(m.Groups[1].Value, "");
                }
            } while (mc.Count > 0);

            return detection;
        }

        public Dictionary<DirInfo, bool> DetectMatches(List<DirInfo> namefiles, List<String> detection)
        {
            Dictionary<DirInfo, bool> fileToOccurenceDict = namefiles.ToDictionary(x => x, x => detection.Select(f => f).Any(s => x.Name.Contains(s)));

            return fileToOccurenceDict;

        }

        public void ReplaceMatches(List<DirInfo> namefiles, String pattern, String replacement)
        {

            Regex r = new Regex(pattern);

            foreach (DirInfo namefile in namefiles)
            {
                FileInfo fileinfo = new FileInfo(workdir + "\\" + namefile.Name);
                String NewNamefile = r.Replace(namefile.Name, replacement);
                try {
                    fileinfo.MoveTo(workdir + "\\" + NewNamefile);
                } catch(Exception e)
                {
                    //rien
                }
            }
        }



        private void BTReplace_Click(object sender, RoutedEventArgs e)
        {
            ReplaceMatches(childFileList, TBPattern.Text, TBReplace.Text);
        }

        private void LVWord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVWord.SelectedItems.Count > 0)
            {
                List<String> selection = new List<String>();
                foreach (var item in LVWord.SelectedItems)
                {
                    var key = item.GetType().GetProperty("Key").GetValue(item, null);
                    selection.Add((String)key);
                }

                Dictionary<DirInfo, bool> matchingfiles = DetectMatches(childFileList, selection);

                LVMatch.ItemsSource = matchingfiles.Where(f => f.Value).Select(f => f.Key);
                LVUnmatch.ItemsSource = matchingfiles.Where(f => !f.Value).Select(f => f.Key);
            }
        }

        private void TBLength_TextChanged(object sender, TextChangedEventArgs e)
        {

            int length = 0;
            if (!int.TryParse(TBLength.Text, out length))
            {
                length = 3;
            }

            Dictionary<String, int> detection = DetectOccurences(childFileList, length);

            LVWord.ItemsSource = detection;
        }
    }


}
