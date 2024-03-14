using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OLIMP.Entities;
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

namespace OLIMP.Views
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = txtLogin.Text;
            var password = txtPassword.Password;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Вы ввели пустой ЛОГИН","ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Вы ввели пустой ПАРОЛЬ", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            User user;
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                 user = context.User.Where(u => u.Login == login && u.Password == password).Include(u => u.Role).FirstOrDefault();
            }
            if (user == null)
            {

                MessageBox.Show("пользователь не найден", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.Role.Name == "Администратор")
            {
                var adminWindow = new AdminWindow();
                adminWindow.Show();
                Close();
            }
            if (user.Role.Name == "Менеджер")
            {
                var managerwindow = new ManagerWindow();
                managerwindow.Show();
                Close();
            }

        }
    }
}
