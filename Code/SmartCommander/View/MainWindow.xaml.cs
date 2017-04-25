using SmartCommander.ViewModel;
using System;
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
using System.Windows.Shapes;

namespace SmartCommander.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = this.DataContext as MainWindowViewModel;
        }


        private void textBoxSearch1_KeyUp(object sender, KeyEventArgs e)
        {
            _viewModel.Explorer0.SearchText = textBoxSearch1.Text;
            _viewModel.Explorer0.RefreshCurrentItems();
        }
        
        private void textBoxSearch2_KeyUp(object sender, KeyEventArgs e)
        {
            _viewModel.Explorer1.SearchText = textBoxSearch2.Text;
            _viewModel.Explorer1.RefreshCurrentItems();
        }

        private void buttonRename1_Click(object sender, RoutedEventArgs e)
        {
            AdvancedSearch advancedSearch = new AdvancedSearch(_viewModel.Path0);
            advancedSearch.Show();
        }

        private void buttonRename2_Click(object sender, RoutedEventArgs e)
        {
            AdvancedSearch advancedSearch = new AdvancedSearch(_viewModel.Path1);
            advancedSearch.Show();
        }
    }
}
