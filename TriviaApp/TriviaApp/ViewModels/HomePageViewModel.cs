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
            Title = $"{u.NickName}'s Home Page";
        }
        public User CurrentUser { get; set; }
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }
        public Action<Page> StartGameEvent;
        public ICommand StartGameCommand => new Command(StartGame);
        private async void StartGame()
        {
            QuestionPage p = new QuestionPage();
            p.BindingContext = await QuestionPageViewModel.CreateQuestionPageViewModel(CurrentUser);
            p.SetEventsAndElements();
            StartGameEvent(p);
        }
        public Action<Page> MoveToMyQuestionsEvent;
        public ICommand MoveToMyQuestionsPageCommand => new Command(ToMyQuestionsPage);
        private async void ToMyQuestionsPage()
        {
            MyQuestionsPage p = new MyQuestionsPage();
            p.BindingContext = await MyQuestionsPageViewModel.CreateMyQuestionsViewModel(CurrentUser);
            p.SetEvents();
            MoveToMyQuestionsEvent(p);
        }
    }
}
