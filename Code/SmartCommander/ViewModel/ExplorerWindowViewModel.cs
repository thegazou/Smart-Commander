﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SmartCommander.Model;
using System.IO;
using System.Collections.Specialized;
using SmartCommander.Properties;
using System.Windows.Input;
using SmartCommander.View;

namespace SmartCommander.ViewModel
{
    public class ExplorerWindowViewModel : ViewModelBase
    {
        #region // Private Members
        private DirInfo _currentDirectory;
        private FileExplorerViewModel _fileTreeVM;
        private DirectoryViewerViewModel _dirViewerVM;
        private IList<DirInfo> _currentItems;
        private bool _showDirectoryTree = true;
        private ICommand _showTreeCommand;
        private MainWindowViewModel _mainWindowVM;
        private int id;
        private String _searchText = "";

        #endregion

        #region // .ctor
        public ExplorerWindowViewModel(int id)
        {
            Id = id;
            FileTreeVM = new FileExplorerViewModel(this);
            DirViewVM = new DirectoryViewerViewModel(this);
            ShowTreeCommand = new RelayCommand(param => this.DirectoryTreeHideHandler());
        }
        #endregion

        #region // Public Properties


        public String SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }


        public MainWindowViewModel MainWindowVM
        {
            get { return _mainWindowVM; }
            set { _mainWindowVM = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Name of the current directory user is in
        /// </summary>
        public DirInfo CurrentDirectory
        {
            get { return _currentDirectory; }
            set
            {
                _currentDirectory = value;
                RefreshCurrentItems();
                OnPropertyChanged("CurrentDirectory");
            }
        }

        /// <summary>
        /// Tree View model
        /// </summary>
        public FileExplorerViewModel FileTreeVM
        {
            get { return _fileTreeVM; }
            set
            {
                _fileTreeVM = value;
                OnPropertyChanged("FileTreeVM");
            }
        }


        /// <summary>
        /// Visibility of the 
        /// </summary>
        public bool ShowDirectoryTree
        {
            get { return _showDirectoryTree; }
            set
            {
                _showDirectoryTree = value;
                OnPropertyChanged("ShowDirectoryTree");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public ICommand ShowTreeCommand
        {
            get { return _showTreeCommand; }
            set
            {
                _showTreeCommand = value;
                OnPropertyChanged("ShowTreeCommand");
            }
        }

        /// <summary>
        /// Tree View model
        /// </summary>
        public DirectoryViewerViewModel DirViewVM
        {
            get { return _dirViewerVM; }
            set
            {
                _dirViewerVM = value;
                OnPropertyChanged("DirViewVM");
            }
        }

        /// <summary>
        /// Children of the current directory to show in the right pane
        /// </summary>
        public IList<DirInfo> CurrentItems
        {
            get
            {
                if (_currentItems == null)
                {
                    _currentItems = new List<DirInfo>();
                }
                return _currentItems;
            }
            set
            {
                _currentItems = value;
                OnPropertyChanged("CurrentItems");
            }
        } 
        #endregion

        #region // methods
        private void DirectoryTreeHideHandler()
        {
            ShowDirectoryTree = false;
        }

        /// <summary>
        /// this method gets the children of current directory and stores them in the CurrentItems Observable collection
        /// </summary>
        public void RefreshCurrentItems()
        {
            IList<DirInfo> childDirList = new List<DirInfo>();
            IList<DirInfo> childFileList = new List<DirInfo>();

            //If current directory is "My computer" then get the all logical drives in the system
            if (CurrentDirectory.Name.Equals(Resources.My_Computer_String))
            {
                childDirList = (from rd in FileSystemExplorerService.GetRootDirectories()
                                select new DirInfo(rd)).ToList();
            }
            else
            {
                //Combine all the subdirectories and files of the current directory
                childDirList = (from dir in FileSystemExplorerService.GetChildDirectories(CurrentDirectory.Path)
                                select new DirInfo(dir)).ToList();

                childFileList = (from fobj in FileSystemExplorerService.GetChildFiles(CurrentDirectory.Path)
                                 select new DirInfo(fobj)).ToList();

                childDirList = childDirList.Concat(childFileList).ToList();
            }

            //Filter the children
            if(_searchText != "")
            {
                for(int i = 0; i < childDirList.Count; i++)
                {
                    if(!childDirList.ElementAt(i).Name.ToLower().Contains(_searchText.ToLower()))
                    {
                        childDirList.RemoveAt(i);
                        i--;
                    }
                }
            }


            CurrentItems = childDirList;
        } 
        #endregion
    }
}
