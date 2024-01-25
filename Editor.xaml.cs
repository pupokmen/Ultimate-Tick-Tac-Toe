using System;
using System.Collections.Generic;
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
    
    public partial class Editor : Window
    {
        public EditorViewModel viewModel;

        public Editor()
        {
            InitializeComponent();

            viewModel = new EditorViewModel(new MVVM.Model.Test("Название теста", new List<Question>()));

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
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Questions.Add(new Question());
        }

        private void ButtonDelQuestion_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Questions.RemoveAt(viewModel.Questions.Count - 1);
        }

        private void ButtonNewAns_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedQuestion.Answers == null)
            {
                viewModel.SelectedQuestion.Answers = new List<Answer>();
            }
            viewModel.SelectedQuestion.Answers.Add(new Answer());
        }

        private void ButtonDelAns_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedQuestion.Answers.Count != 0)
                viewModel.SelectedQuestion.Answers.RemoveAt(viewModel.SelectedQuestion.Answers.Count - 1);
            else
                MessageBox.Show("Количество вопросов равно 0", "Error");
        }


        private void ButtonDecline_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Questions.Clear();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            Test test = new Test(TestName.Text, this.viewModel.Questions.ToList());
            Test.Serialize(test);

            MessageBox.Show("Тест успешно сохранен");
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in viewModel.SelectedQuestion.Answers)
            {
                item.AnsStatus = false;
                ((CheckBox)sender).IsChecked = true;
            }
        }

    }
}
