using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SmartCommander.View;
using SmartCommander.ViewModel;

namespace SmartCommander
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            /*
            ExplorerWindow window = new ExplorerWindow();

            // ViewModel to bind the main window 
            ExplorerWindowViewModel viewModel = new ExplorerWindowViewModel();

            // Allow all controls in the window to bind to the ViewModel by setting the 
            // DataContext, which propagates down the element tree.
            window.DataContext = viewModel;

            window.Show();*/

            MainWindow mainWindow = new MainWindow();
            // ViewModel to bind the main window 
            ExplorerWindowViewModel viewModel = new ExplorerWindowViewModel();
            ExplorerWindowViewModel viewModel2 = new ExplorerWindowViewModel();

            // Allow all controls in the window to bind to the ViewModel by setting the 
            // DataContext, which propagates down the element tree.
            mainWindow.Explorer.DataContext = viewModel;
            mainWindow.Explorer2.DataContext = viewModel2;


            mainWindow.Show();
        }
    }
}
