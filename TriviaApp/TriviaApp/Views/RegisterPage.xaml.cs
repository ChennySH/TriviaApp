using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            this.BindingContext = new RegisterPageViewModel();
            ((RegisterPageViewModel)this.BindingContext).RegisterAction += OpenLogInPage;
            InitializeComponent();
        }
        public async void OpenLogInPage(Page p)
        {
            await Navigation.PushAsync(p);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.BindingContext = new RegisterPageViewModel();
            ((RegisterPageViewModel)this.BindingContext).RegisterAction += OpenLogInPage;
        }
    }
}