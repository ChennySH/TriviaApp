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
    public partial class MyQuestionsPage : ContentPage
    {
        bool appeard;
        public MyQuestionsPage()
        {
            InitializeComponent();
            appeard = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!appeard)
            {
                appeard = true;
            }
            else
            {
                ((MyQuestionsPageViewModel)this.BindingContext).Reset();
            }
        }
    }
}