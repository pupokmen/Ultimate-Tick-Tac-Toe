using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UTS_2._0.MVVM.Model;

namespace UTS_2._0.MVVM.ViewModel
{
    public class EditorViewModel : INotifyPropertyChanged
    {
        private Question selectedQuestion;
        public ObservableCollection<Question> Questions { get; set; }
        public List<Attempt> attempts { get; set; }
        public Test Test;

        public Question SelectedQuestion
        {
            get { return selectedQuestion; }
            set
            {
                selectedQuestion = value;
                OnPropertyChanged("SelectedQuestion");
            }
        }

        public EditorViewModel(Test test)
        {
            Test = test;
            Questions = new ObservableCollection<Question>(test.Questions);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
