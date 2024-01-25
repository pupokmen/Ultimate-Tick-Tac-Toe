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
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public MainViewModel viewModel;

        public Statistics()
        {
            InitializeComponent();

            viewModel = ((MainWindow)Application.Current.MainWindow).viewModel;

            DataContext = viewModel;
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

        private void ButtonRename_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox1.IsEnabled == false)
            {
                TextBox1.IsEnabled = true;
                TextBox1.Focus();
            }
            else
            {
                TextBox1.IsEnabled = false;

                using (ApplicationContext db = new ApplicationContext())
                {
                    List<User> Users = db.Users.ToList();

                    foreach (var user in db.Users)
                    {
                        if (user.Id == viewModel.User.First().Id)
                            user.Login = viewModel.User.First().Login;
                    }

                    db.SaveChanges();
                    db.Dispose();
                }
            }
        }
    }
}
