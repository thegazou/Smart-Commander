using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SmartCommander.Model;

namespace SmartCommander.ViewModel
{
    /// <summary>
    /// View model for the right side pane
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private ExplorerWindowViewModel[] explorersVM = new ExplorerWindowViewModel[2];
        private String[] path = new String[2];
        public String Path0 {
            get { return path[0]; }
            set
            {
                path[0] = value;
                OnPropertyChanged("Path0");
            }
        }
        public String Path1
        {
            get { return path[1]; }
            set {
                path[1] = value;
                OnPropertyChanged("Path1");
            }
        }

        public MainWindowViewModel(ExplorerWindowViewModel evm, ExplorerWindowViewModel evm2)
        {
            explorersVM[0] = evm;
            explorersVM[1] = evm2;
        }

        public void setPath(int id, String path)
        {
            this.path[id] = path;
            OnPropertyChanged("Path" + id);
        }
    }
}
