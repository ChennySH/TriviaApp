using System;
using TriviaApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new LogInPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
