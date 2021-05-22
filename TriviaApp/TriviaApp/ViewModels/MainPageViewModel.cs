using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TriviaApp.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Action PressLoginEvent;
        public Action PressRegisterEvent;
        public ICommand PressLogInCommand => new Command(PressLogin);
        private void PressLogin()
        {
            PressLoginEvent();
        }
        public ICommand PressRegisterCommand => new Command(PressRegister);

        private void PressRegister()
        {
            PressRegisterEvent();
        }
    }
}
