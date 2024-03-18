using OLIMP.Entities;
using OLIMP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OLIMP.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private ManagerViewModel _viewModel; 
        public ManagerWindow(User user)
        {
            InitializeComponent();
            _viewModel = new ManagerViewModel(user);
            this.DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
