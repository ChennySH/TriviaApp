using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TriviaApp.Models;
using TriviaApp.Services;
using Xamarin.Forms;

namespace TriviaApp.ViewModels
{
    class CreateQuestionPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private TriviaAppWebApiProxy proxy;
        private User currentUser;
        #region textProperties
        private string questionText;
        public string QuestionText
        {
            get
            {
                return questionText;
            }
            set
            {
                if(questionText != value)
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
                if(correctAnswer != value)
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
                if(incorrectAnswer1 != value)
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
                if(incorrectAnswer2 != value)
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
                if(incorrectAnswer3 != value)
                {
                    incorrectAnswer3 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string errorMessege;
        public string ErrorMessege
        {
            get
            {
                return errorMessege;
            }
            set
            {
                if(errorMessege != value)
                {
                    errorMessege = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        public CreateQuestionPageViewModel(User u)
        {
            currentUser = u;
            questionText = "";
            correctAnswer = "";
            incorrectAnswer1 = "";
            incorrectAnswer2 = "";
            incorrectAnswer3 = "";
            errorMessege = "";
            proxy = TriviaAppWebApiProxy.CreateProxy();
        }
        public ICommand CreateQuestionCommand => new Command(CreateQuestion);

        private async void CreateQuestion()
        {
            if(questionText != "" && correctAnswer != "" && incorrectAnswer1 != ""
                && incorrectAnswer2 != "" && incorrectAnswer3 != "")
            {
                AmericanQuestion q = new AmericanQuestion 
                { 
                    CreatorNickName = 
                    currentUser.NickName, 
                    QText = questionText, 
                    CorrectAnswer = correctAnswer, 
                    OtherAnswers = new string[3],
                };
                q.OtherAnswers[0] = incorrectAnswer1;
                q.OtherAnswers[1] = incorrectAnswer2;
                q.OtherAnswers[2] = incorrectAnswer3;
                bool posted = await proxy.PostNewQuestionAsync(q);
                if (posted)
                {

                }
                else
                {

                }
            }
        }
    }
}
