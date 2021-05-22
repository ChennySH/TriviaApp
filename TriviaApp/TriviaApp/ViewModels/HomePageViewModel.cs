using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TriviaApp.Models;

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
    }
}
