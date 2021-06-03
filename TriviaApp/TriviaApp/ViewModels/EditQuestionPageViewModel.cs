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
        private AmericanQuestion question;
        public EditQuestionPageViewModel(AmericanQuestion q, User user)
        {
            this.ErrorMessege = "";
            this.question = q;
            this.currentUser = user;
            this.proxy = TriviaAppWebApiProxy.CreateProxy();
            this.QuestionText = question.QText;
            this.CorrectAnswer = question.CorrectAnswer;
            this.IncorrectAnswer1 = question.OtherAnswers[0];
            this.IncorrectAnswer2 = question.OtherAnswers[1];
            this.IncorrectAnswer3 = question.OtherAnswers[2];
        }
        public ICommand ResetEntriesCommand => new Command(ResetEntries);

        private void ResetEntries()
        {
            this.QuestionText = question.QText;
            this.CorrectAnswer = question.CorrectAnswer;
            this.IncorrectAnswer1 = question.OtherAnswers[0];
            this.IncorrectAnswer2 = question.OtherAnswers[1];
            this.IncorrectAnswer3 = question.OtherAnswers[2];
            this.ErrorMessege = "";
        }
        public ICommand EditQuestionCommand => new Command(EditQuestion);
        private async void EditQuestion()
        {
            if (questionText != "" && correctAnswer != "" && incorrectAnswer1 != ""
                && incorrectAnswer2 != "" && incorrectAnswer3 != "")
            {
                AmericanQuestion q = new AmericanQuestion
                {
                    CreatorNickName = currentUser.NickName,
                    QText = questionText,
                    CorrectAnswer = correctAnswer,
                    OtherAnswers = new string[3],
                };
                q.OtherAnswers[0] = incorrectAnswer1;
                q.OtherAnswers[1] = incorrectAnswer2;
                q.OtherAnswers[2] = incorrectAnswer3;
                bool originalDeleted = await this.proxy.DeleteQuestionAsync(this.question);
                if (originalDeleted)
                {
                    await this.proxy.PostNewQuestionAsync(q);
                    PopAction();
                }
                else
                {
                    this.ErrorMessege = "Somthing went worng";
                }
            }
            else
            {
                this.ErrorMessege = "Please fill all the entries";
            }
        }
        public Action PopAction;
    }
}
