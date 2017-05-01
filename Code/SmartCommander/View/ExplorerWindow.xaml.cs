using SmartCommander.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartCommander.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ExplorerWindow : UserControl
    {

        private ExplorerWindowViewModel myViewModel;
        private static String copyPath;

        public ExplorerWindowViewModel ViewModel
        {
            set { myViewModel = value; }
        }

        public ExplorerWindow()
        {
            InitializeComponent();
       }

        void CopyCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            copyPath = myViewModel.MainWindowVM.getPath(myViewModel.Id);
        }

        void CopyCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void PasteCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            String fileName = copyPath.Split('\\').Last();
            File.Copy(copyPath, myViewModel.FileTreeVM.CurrentTreeItem.Path + "\\" + fileName);
        }

        void PasteCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = copyPath != null;
        }

    }
}
