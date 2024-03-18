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
    public partial class ContractWindow : Window
    {
        private ContractViewModel _viewModel;
        public ContractWindow(Client selectedClient, User user)
        {
            InitializeComponent();

            _viewModel = new ContractViewModel(selectedClient, user);

            this.DataContext = _viewModel;
        }
    }
}
