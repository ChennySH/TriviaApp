using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TriviaApp.Models;
using TriviaApp.Services;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;

namespace TriviaApp.ViewModels
{
    class EditQuestionPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private User currentUser;
        private TriviaAppWebApiProxy proxy;
        private string questionText;
        public string QuestionText
        {
            get
            {
                return questionText;
            }
            set
            {
                if (questionText != value)
                {
                    questionText = value;
                    OnPropertyChanged();
                }
            }
        }
        private string correctAnswer;
        public string CorrectAnswer
        {
            get
            {
                return correctAnswer;
            }
            set
            {
                if (correctAnswer != value)
                {
                    correctAnswer = value;
                    OnPropertyChanged();
                }
            }
        }
        private string incorrectAnswer1;
        public string IncorrectAnswer1
        {
            get
            {
                return incorrectAnswer1;
            }
            set
            {
                if (incorrectAnswer1 != value)
                {
                    incorrectAnswer1 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string incorrectAnswer2;
        public string IncorrectAnswer2
        {
            get
            {
                return incorrectAnswer2;
            }
            set
            {
                if (incorrectAnswer2 != value)
                {
                    incorrectAnswer2 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string incorrectAnswer3;
        public string IncorrectAnswer3
        {
            get
            {
                return incorrectAnswer3;
            }
            set
            {
                if (incorrectAnswer3 != value)
                {
                    incorrectAnswer3 = value;
                    OnPropertyChanged();
                }
            }
        }
        private AmericanQuestion question;
        public EditQuestionPageViewModel(AmericanQuestion q, User u)
        {
            this.currentUser = u;
            this.question = q;
            this.proxy = TriviaAppWebApiProxy.CreateProxy();
            this.QuestionText = question.QText;
            this.CorrectAnswer = question.CorrectAnswer;
            this.IncorrectAnswer1 = question.OtherAnswers[0];
            this.IncorrectAnswer2 = question.OtherAnswers[1];
            this.IncorrectAnswer3 = question.OtherAnswers[2];
        }
        public ICommand EditQuestionCommand => new Command(EditQuestion);

        private void EditQuestion()
        {
            
        }
    }
}
