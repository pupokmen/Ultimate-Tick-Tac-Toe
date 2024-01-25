using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UTS_2._0.MVVM.Model;
using UTS_2._0.MVVM.ViewModel;

namespace UTS_2._0
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            ВойтиButton.IsChecked = true;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ВойтиButton_Click(object sender, RoutedEventArgs e)
        {
            if (ВойтиButton.IsChecked == true) 
            { 
                ЗарегистрироватьсяButton.Opacity = 0.5;
                ВойтиButton.Opacity = 1;
            }
            TextBox1.Text = string.Empty;
            PasswordBox1.Password = string.Empty;
        }

        private void ЗарегистрироватьсяButton_Click(object sender, RoutedEventArgs e)
        {
            if (ЗарегистрироватьсяButton.IsChecked == true) 
            { 
                ВойтиButton.Opacity = 0.5;
                ЗарегистрироватьсяButton.Opacity = 1;
            }
            TextBox1.Text = string.Empty;
            PasswordBox1.Password = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ВойтиButton.IsChecked == true)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    List<User> users = new List<User>();
                    users.AddRange(db.Users);

                    foreach (var user in users)
                    {
                        if (user.Login == TextBox1.Text && user.Password == PasswordBox1.Password)
                        {
                            ((MainWindow)Application.Current.MainWindow).viewModel.User.First().Login = user.Login;
                            ((MainWindow)Application.Current.MainWindow).viewModel.User.First().Password = user.Password;
                            ((MainWindow)Application.Current.MainWindow).viewModel.User.First().Id = user.Id;

                            this.Close();
                        }
                    }

                    db.Dispose();
                }
            }
            else
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User user = new User
                    {
                        Login = TextBox1.Text.ToString().TrimStart().TrimEnd(),
                        Password = PasswordBox1.Password.ToString().TrimStart().TrimEnd()
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    db.Dispose();

                    MessageBox.Show("Регистрация прошла успешно!");
                }
            }
        }
    }
}
