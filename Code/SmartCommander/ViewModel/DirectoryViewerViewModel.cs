namespace SmartCommander.ViewModel
{
    /// <summary>
    /// View model for the right side pane
    /// </summary>
    public class DirectoryViewerViewModel : ViewModelBase
    {
        #region // Private variables
        private ExplorerWindowViewModel _evm;
        private DirInfo _currentItem;
        #endregion

        #region // .ctor
        public DirectoryViewerViewModel(ExplorerWindowViewModel evm)
        {
            _evm = evm;
        }
        #endregion

        #region // Public members
        /// <summary>
        /// Indicates the current directory in the Directory view pane
        /// </summary>
        public DirInfo CurrentItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                _evm.MainWindowVM.setPath(_evm.Id, _currentItem.Path);
            }
        }
        #endregion

        #region // Public Methods
        /// <summary>
        /// processes the current object. If this is a file then open it or if it is a directory then return its subdirectories
        /// </summary>
        public void OpenCurrentObject()
        {
            if (CurrentItem != null)
            {
                int objType = CurrentItem.DirType; //Dir/File type

                if ((ObjectType)CurrentItem.DirType == ObjectType.File)
                {
                    System.Diagnostics.Process.Start(CurrentItem.Path);
                }
                else
                {
                    _evm.CurrentDirectory = CurrentItem;
                    _evm.FileTreeVM.ExpandToCurrentNode(_evm.CurrentDirectory);
                }
            }
        }
        #endregion
    }
}
