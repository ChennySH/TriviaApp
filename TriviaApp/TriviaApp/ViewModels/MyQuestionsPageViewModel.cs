using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaApp.Models;
using TriviaApp.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TriviaApp.ViewModels
{
   class MyQuestionsPageViewModel : INotifyPropertyChanged
    {
        private TriviaAppWebApiProxy proxy;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public User currentUser { get; set; }
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<AmericanQuestion> QuestionsList { get; set; }
        public static async Task<MyQuestionsPageViewModel> CreateMyQuestionsViewModel(User user)
        {
            TriviaAppWebApiProxy proxy = TriviaAppWebApiProxy.CreateProxy();
            string email = user.Email;
            string password = user.Password;
            User updated = await proxy.LoginAsync(email, password);
            return new MyQuestionsPageViewModel(updated);
        }
        private MyQuestionsPageViewModel(User u)
        {
            this.proxy = TriviaAppWebApiProxy.CreateProxy();
            this.currentUser = u;
            SetProperties();
        }
        private void SetProperties()
        {
            this.UserName = currentUser.NickName;
            if (this.QuestionsList != null)
                this.QuestionsList.Clear();
            else
                this.QuestionsList = new ObservableCollection<AmericanQuestion>();
            foreach (AmericanQuestion q in currentUser.Questions)
            {
                this.QuestionsList.Add(q);
            }
        }
        public async void Reset()
        {
            string email = currentUser.Email;
            string password = currentUser.Password;
            currentUser = await this.proxy.LoginAsync(email, password);
            SetProperties();
        }
        public ICommand DeleteQuestionCommand => new Command<AmericanQuestion>(DeleteQuestion);
        public async void DeleteQuestion(AmericanQuestion q)
        {
            bool deleted = await this.proxy.DeleteQuestionAsync(q);
            if (deleted)
            {
                Reset();
                //
                //
            }
        }
        public MyQuestionsPageViewModel() { }
    } 
}
