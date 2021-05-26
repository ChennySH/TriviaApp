  using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TriviaApp.Models;
using TriviaApp.Services;
using TriviaApp.Views;
using Xamarin.Forms;

namespace TriviaApp.ViewModels
{
    
    class RegisterPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RegisterPageViewModel()
        {
            email = "";
            password = "";
            repeatedPassword = "";
            nickName = "";
            proxy = TriviaAppWebApiProxy.CreateProxy();
        }
        private TriviaAppWebApiProxy proxy;
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if(email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if(password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }
        private string repeatedPassword;
        public string RepeatedPasseord
        {
            get
            {
                return repeatedPassword;
            }
            set
            {
                if(repeatedPassword != value)
                {
                    repeatedPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        private string nickName;
        public string NickName
        {
            get
            {
                return nickName;
            }
            set
            {
                if(nickName != value)
                {
                    nickName = value;
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
        public ICommand RegisterCommand => new Command(Register);
        public Action<Page> RegisterAction;
        private async void Register()
        {
            if(email != "" && password != "" && nickName != "")
            {
                if (password == repeatedPassword)
                {
                    User u = new User
                    {
                        Email = email,
                        Password = password,
                        NickName = nickName,
                        Questions = new List<AmericanQuestion>()
                    };
                    bool registered = await proxy.RegisterAsync(u);
                    if (registered)
                    {
                        LogInPage p = new LogInPage(email, password);
                        RegisterAction(p);
                    }
                    else
                    {
                        ErrorMessege = "Something went wrong";
                    }
                }
                else
                    ErrorMessege = "Passwords not matching";
            }
            else
            {
                ErrorMessege = "Please enter email, password and nickname";
            }
        }
    }
}
