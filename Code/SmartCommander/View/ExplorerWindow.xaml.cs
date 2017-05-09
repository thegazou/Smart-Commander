using SmartCommander.Model;
using SmartCommander.ViewModel;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartCommander.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ExplorerWindow : UserControl
    {

        private ExplorerWindowViewModel myViewModel;
        private static String copyPath;
        private static bool isCut;

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
            isCut = false;
        }

        void CopyCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (myViewModel.DirViewVM.CurrentItem != null && myViewModel.DirViewVM.CurrentItem.DirType == (int)ObjectType.File)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        void PasteCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            if (isCut)
            {
                MoveFileService.PastFile(copyPath, myViewModel);
                File.Delete(copyPath);
                copyPath = null;
            }
            else
                MoveFileService.PastFile(copyPath, myViewModel);
            myViewModel.RefreshCurrentItems();
        }

        void PasteCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (myViewModel.DirViewVM.CurrentItem != null && copyPath != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        void CutCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (myViewModel.DirViewVM.CurrentItem != null && myViewModel.DirViewVM.CurrentItem.DirType == (int)ObjectType.File)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        void CutCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            copyPath = myViewModel.MainWindowVM.getPath(myViewModel.Id);
            isCut = true;

        }

    }
}
