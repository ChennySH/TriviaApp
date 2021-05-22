using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaApp.ViewModels;
using TriviaApp.Views;
using Xamarin.Forms;

namespace TriviaApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.BindingContext = new MainPageViewModel();
            ((MainPageViewModel)this.BindingContext).PressRegisterEvent += PressRegister;
            ((MainPageViewModel)this.BindingContext).PressLoginEvent += PressLogin;
            InitializeComponent();
        }
        public async void PressLogin()
        {
            LogInPage p = new LogInPage();
            await Navigation.PushAsync(p);
        }
        public async void PressRegister()
        {
            RegisterPage p = new RegisterPage();
            await Navigation.PushAsync(p);
        }
    }
}
