using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaApp.Views;
using TriviaApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaApp.Models;

namespace TriviaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            this.BindingContext = new LoginPageViewModel();
            ((LoginPageViewModel)BindingContext).LogInEvent += OpenHomePage;
            InitializeComponent();
        }
        public LogInPage(string email, string password)
        {
            this.BindingContext = new LoginPageViewModel(email, password);
            ((LoginPageViewModel)BindingContext).LogInEvent += OpenHomePage;
            InitializeComponent();
        }
        public async void OpenHomePage(Page p)
        {
            await Navigation.PushAsync(p);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.BindingContext = new LoginPageViewModel();
            ((LoginPageViewModel)BindingContext).LogInEvent += OpenHomePage;
        }
    }
}