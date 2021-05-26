using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaApp.Models;
using TriviaApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {           
            InitializeComponent();
        }
        public void SetEvents()
        {
            ((HomePageViewModel)this.BindingContext).StartGameEvent += StartGame;
        }
        public async void StartGame(Page p)
        {
            await Navigation.PushAsync(p);
        }
    }
}