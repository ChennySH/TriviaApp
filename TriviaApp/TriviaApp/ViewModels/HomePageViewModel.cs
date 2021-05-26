using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TriviaApp.Models;
using Xamarin.Forms;
using TriviaApp.Views;

namespace TriviaApp.ViewModels
{
    class HomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public HomePageViewModel(User u)
        {
            CurrentUser = u;
            userName = u.NickName;
        }
        public User CurrentUser { get; set; }
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (value != userName)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }
        public Action<Page> StartGameEvent;
        public ICommand StartGameCommand => new Command(StartGame);

        private void StartGame()
        {
            QuestionPage p = new QuestionPage();
            p.BindingContext = new QuestionPageViewModel(CurrentUser);
            ((QuestionPageViewModel)p.BindingContext).ResetQuestion();
            p.SetEventsAndElements();
            StartGameEvent(p);
        }
    }
}
