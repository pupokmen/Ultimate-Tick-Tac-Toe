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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UTS_2._0.MVVM.Model;
using UTS_2._0.MVVM.ViewModel;

namespace UTS_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            //using (ApplicationContext db = new ApplicationContext())
            //{
            //    List<User> users = new List<User>()
            //    {
            //        new User() { Login = "логин1", Password = "пароль1", Attempts = new List<Attempt> { } },
            //        new User() { Login = "логин2", Password = "пароль2", Attempts = new List<Attempt> { } }
            //    };

            //    List<Attempt> attempts = new List<Attempt>()
            //    {
            //        new Attempt() { TestName = "тест1", Result = 9, User = "логин1"},
            //        new Attempt() { TestName = "тест2", Result = 2, User = "логин1"}
            //    };

            //    db.Users.AddRange(users);
            //    db.Attempts.AddRange(attempts);
            //    db.SaveChanges();


            //    db.Dispose();
            //}

            //List<Question> Questions = new List<Question>()
            //{
            //    new Question {Name = "1)", Description = "Сколько лап у многоножки?", Content = "Сколько лап у многоножки? Дать один ответ в виде цифры.", Answers = new Answer[5] { new Answer { Ans = "2"}, new Answer { Ans = "3"}, new Answer { Ans = "< 40"}, new Answer { Ans = "> 40"}, new Answer { Ans = "40"} }, TrueAnswer = 5 },
            //    new Question {Name = "2)", Description = "Можно ли пить сладкую воду?", Content = "В современных супермаркетах полки с разнообразной цветной газировкой просто ломятся от тяжести, пестрят разнообразием оттенков и фруктовыми вкусами, так и манят покупателя. Но так ли привлекательна сладкая газировка, как кажется внешне? Почему нельзя пить сладкую газировку? Можно ли пить сладкую воду?", Answers = new Answer[2] { new Answer { Ans = "ДА"}, new Answer { Ans = "НЕТ"} }, TrueAnswer = 2 },
            //    new Question {Name = "3)", Description = "Сколько лап у лягушки?", Content = "Сколько лап у лягушки? Дать один ответ в виде цифры.", Answers = new Answer[2] { new Answer { Ans = "3"}, new Answer { Ans = "4"} }, TrueAnswer = 2},
            //    new Question {Name = "4)", Description = "ДА или НЕТ?", Content = "Да или Нет?", Answers = new Answer[2] { new Answer { Ans = "ДА!"}, new Answer { Ans = "Нет..."} }, TrueAnswer = 1},
            //    new Question {Name = "5)", Description = "Почему бананы желтые?", Content = "В процессе созревания банана резистентный крахмал разрушается и превращается в простые сахара. Правда ли, что из-за этого они становяться желтыми?", Answers = new Answer[2] { new Answer { Ans = "Да"}, new Answer { Ans = "Нет"} }, TrueAnswer = 1}
            //};

            //List<Question> questions = new List<Question>()
            //{
            //    new Question {Name = "3)", Description = "Сколько лап у лягушки?", Content = "Сколько лап у лягушки?\n\n\n\n\n\n\nДать ответ в виде цифры.", Answers = new Answer[2] { new Answer { Ans = "3"}, new Answer { Ans = "4"} }, TrueAnswer = 2}
            //};

            //Test Test = new Test("Тест1", Questions);
            //Test.Serialize(Test);

            viewModel = new MainViewModel(new Test(), new User { Login = "Войти" , Attempts = new List<Attempt>()});
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
            Application.Current.Shutdown();
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Test.Name == "")
            {
                Test test = Test.LoadTest();
                viewModel.Test.Name = test.Name;

                foreach (var item in test.Questions)
                {
                    viewModel.Questions.Add(item);
                }

                //MessageBox.Show(test.Name);
            }
            else if (viewModel.User.First().Password != null)
            {
                double res = 0;
                double ansCost = Math.Round(10 / (double)viewModel.Questions.Count, 3);

                for (int i = 0; i < viewModel.Questions.Count; i++)
                {
                    for (int j = 0; j < viewModel.Questions[i].Answers.Count; j++)
                    {
                        if (viewModel.Questions[i].Answers[j].AnsStatus == true && viewModel.Questions[i].TrueAnswer == j + 1)
                            res += ansCost;
                    }
                }

                Attempt attempt = new Attempt
                {
                    TestName = viewModel.Test.Name,
                    Result = (int)Math.Round(res, 2),
                    User = viewModel.User.First().Login
                };

                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Attempts.Add(attempt);
                    db.SaveChanges();

                    db.Dispose();
                }

                MessageBox.Show($"Вы прошли тест {viewModel.Test.Name} с оценкой: {res}/10. Well Done!", "Результат");
            }
            else
                MessageBox.Show("Войдите в аккаунт для прохождения теста.", "Ошибка");
        }

        private void ButtonDecline_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Questions.Count > 0)
                viewModel.Questions.Clear();
            viewModel.Test.Name = "";
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            foreach (var question in viewModel.Questions)
            {
                foreach (var answer in question.Answers)
                {
                    answer.AnsStatus = false;
                }
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            viewModel.User.First().Attempts.Clear();

            if (viewModel.User.First().Password == null)
            {
                Login login = new Login();
                login.ShowDialog();

                foreach (var q in viewModel.Questions)
                {
                    foreach (var a in q.Answers)
                    {
                        a.AnsStatus = false;
                    }
                }
            }
            else
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var item in db.Attempts)
                    {
                        if (item.User == viewModel.User.First().Login)
                        {
                            viewModel.User.First().Attempts.Add(item);
                        }
                    }

                    db.Dispose();
                }

                viewModel.User.First().CalcAvg();

                Statistics statistics = new Statistics();
                statistics.Show();
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in viewModel.SelectedQuestion.Answers)
            {
                item.AnsStatus = false;
                ((CheckBox)sender).IsChecked = true;
            }
        }

        private void ButtonNewTest_Click(object sender, RoutedEventArgs e)
        {
            Editor editor = new Editor();
            editor.ShowDialog();
        }
    }
}
