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
using TriviaApp.Views;

namespace TriviaApp.ViewModels
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        public LoginPageViewModel()
        {
            this.email = "";
            this.password = "";
            proxy = TriviaAppWebApiProxy.CreateProxy();
        }
        public LoginPageViewModel(string email, string password)
        {
            this.email = email;
            this.password = password;
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
        public ICommand LoginCommand => new Command(LoginMethod);
        public Action<Page> LogInEvent;
        private async void LoginMethod()
        {
            if (this.email != "" && this.password != "")
            {
                User currentUser = await this.proxy.LoginAsync(this.email, this.password);
                if (currentUser != null)
                {
                    HomePage p = new HomePage();
                    p.BindingContext = new HomePageViewModel(currentUser);
                    LogInEvent(p);
                }
                else
                {
                    ErrorMessege = "Email or Password incorrect";
                }
            }
            else
                ErrorMessege = "Please enter email and password";
        }
    }
}
