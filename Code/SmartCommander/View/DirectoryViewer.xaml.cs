using System;
using System.Linq;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using SmartCommander.ViewModel;
using SmartCommander.Model;

namespace SmartCommander.View
{
    /// <summary>
    /// Interaction logic for DirectoryViewer.xaml
    /// </summary>
    public partial class DirectoryViewer : UserControl
    {
        #region // Private members
        private ExplorerWindowViewModel _viewModel;
        private static String copyPath;
        private static bool isCut;
        #endregion

        #region // .ctor
        public DirectoryViewer()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(DirectoryViewer_Loaded);
        }
        #endregion

        #region // Event Handlers
        void DirectoryViewer_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = this.DataContext as ExplorerWindowViewModel;
        }

        private void dirList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.DirViewVM.OpenCurrentObject();
        }

        private void dirList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.DirViewVM.OpenCurrentObject();
            }
        }
        #endregion

        private void dirList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.DirViewVM.CurrentItem != null)
                _viewModel.MainWindowVM.setPath(_viewModel.Id, _viewModel.DirViewVM.CurrentItem.Path);
        }

        private void CxmOpened(Object sender, RoutedEventArgs args)
        {
            if (_viewModel.DirViewVM.CurrentItem != null)
            {
                if (_viewModel.DirViewVM.CurrentItem.DirType != (int)ObjectType.File)
                {
                    cxmItemCut.IsEnabled = false;
                    cxmItemCopy.IsEnabled = false;
                    cxmItemDelete.IsEnabled = false;
                }
                else
                {
                    cxmItemCut.IsEnabled = true;
                    cxmItemCopy.IsEnabled = true;
                    cxmItemDelete.IsEnabled = true;
                }
                cxmItemNouveau.IsEnabled = true;
                if (copyPath == null)
                    cxmItemPaste.IsEnabled = false;
                else
                    cxmItemPaste.IsEnabled = true;
            }
            else
            {
                cxmItemPaste.IsEnabled = false;
                cxmItemCut.IsEnabled = false;
                cxmItemCopy.IsEnabled = false;
                cxmItemDelete.IsEnabled = false;
                cxmItemNouveau.IsEnabled = false;
            }


        }

        private void ClickCopy(Object sender, RoutedEventArgs args)
        {
            if (_viewModel.DirViewVM.CurrentItem != null && _viewModel.DirViewVM.CurrentItem.DirType == (int)ObjectType.File)
            {
                copyPath = _viewModel.MainWindowVM.getPath(_viewModel.Id);
                _viewModel.RefreshCurrentItems();
                isCut = false;
            }

        }

        private void ClickPaste(Object sender, RoutedEventArgs args)
        {
            if (_viewModel.DirViewVM.CurrentItem != null && copyPath != null)
            {
                if (isCut)
                {
                    MoveFileService.PastFile(copyPath, _viewModel);
                    File.Delete(copyPath);
                    copyPath = null;
                }
                else
                    MoveFileService.PastFile(copyPath, _viewModel);
            }
            _viewModel.RefreshCurrentItems();
        }

        private void ClickNouveau(Object sender, RoutedEventArgs args)
        {
            if (_viewModel.DirViewVM.CurrentItem != null)
                if (_viewModel.DirViewVM.CurrentItem.DirType == (int)ObjectType.Directory)
                    File.Create(_viewModel.MainWindowVM.getPath(_viewModel.Id) + "\\nouveau fichier.txt");
                else
                {
                    String fileName = _viewModel.MainWindowVM.getPath(_viewModel.Id).Split('\\').Last();
                    String temp = _viewModel.MainWindowVM.getPath(_viewModel.Id).Substring(0, _viewModel.MainWindowVM.getPath(_viewModel.Id).LastIndexOf(fileName)) + "nouveau fichier.txt";
                    File.Create(temp);
                }
            _viewModel.RefreshCurrentItems();
        }

        private void ClickDelete(Object sender, RoutedEventArgs args)
        {
            if (_viewModel.DirViewVM.CurrentItem != null && _viewModel.DirViewVM.CurrentItem.DirType == (int)ObjectType.File)
                File.Delete(_viewModel.MainWindowVM.getPath(_viewModel.Id));
            _viewModel.RefreshCurrentItems();
        }

        private void ClickCut(Object sender, RoutedEventArgs args)
        {
            if (_viewModel.DirViewVM.CurrentItem != null && _viewModel.DirViewVM.CurrentItem.DirType == (int)ObjectType.File)
            {
                copyPath = _viewModel.MainWindowVM.getPath(_viewModel.Id);
                _viewModel.RefreshCurrentItems();
                isCut = true;
            }
        }
    }
}
