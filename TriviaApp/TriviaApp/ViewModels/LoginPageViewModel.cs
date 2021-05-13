using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TriviaApp.Models;
using Xamarin.Forms;
using TriviaApp.Services;
using System.Threading.Tasks;

namespace TriviaApp.ViewModels
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        public LoginPageViewModel()
        {
            this.email = "";
            this.password = "";
            currentUser = null;
            proxy = TriviaAppWebApiProxy.CreateProxy();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private TriviaAppWebApiProxy proxy;
        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                if (value != this.email)
                {
                    this.email = value;
                    OnPropertyChanged();
                }
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    OnPropertyChanged();
                }
            }
        }
        private User currentUser;
        public ICommand LoginCommand => new Command(LoginMethod);

        private async void LoginMethod()
        {
            if(this.email != "" && this.password != "" && currentUser == null)
            {
                currentUser = await this.proxy.LoginAsync(this.email, this.password);
            }
            if(currentUser != null)
            {
                
            }
        }
    }
}
