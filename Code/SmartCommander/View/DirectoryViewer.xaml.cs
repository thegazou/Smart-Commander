﻿using System;
using System.Collections.Generic;
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
using SmartCommander.ViewModel;

namespace SmartCommander.View
{
    /// <summary>
    /// Interaction logic for DirectoryViewer.xaml
    /// </summary>
    public partial class DirectoryViewer : UserControl
    {
        #region // Private members
        private ExplorerWindowViewModel _viewModel;
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
        private void dirList_ContextMenu_KeyDown_Cut(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.DirViewVM.OpenCurrentObject();
            }
        }
        private void dirList_ContextMenu_MouseDown_Cut(object sender, MouseButtonEventArgs e)
        {
            _viewModel.DirViewVM.OpenCurrentObject();
        }
        #endregion

        private void dirList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.MainWindowVM.setPath(_viewModel.Id, _viewModel.DirViewVM.CurrentItem.Path);
        }

        private void CxmOpened(Object sender, RoutedEventArgs args)
        {
            /*
            // Only allow copy/cut if something is selected to copy/cut.
            if (_viewModel.CurrentDirectory.DependencyObjectType.SelectedText == "")
                cxmItemCopy.IsEnabled = cxmItemCut.IsEnabled = false;
            else
                cxmItemCopy.IsEnabled = cxmItemCut.IsEnabled = true;
                */
            // Only allow paste if there is text on the clipboard to paste.
            if (Clipboard.ContainsText())
                cxmItemPaste.IsEnabled = true;
            else
                cxmItemPaste.IsEnabled = false;
        }
        private void ClickCopy(Object sender, RoutedEventArgs args)
        {
            //TODO
                }
        private void ClickPaste(Object sender, RoutedEventArgs args)
        {

        }
        private void ClickSelectAll(Object sender, RoutedEventArgs args)
        {

        }
    }
}
