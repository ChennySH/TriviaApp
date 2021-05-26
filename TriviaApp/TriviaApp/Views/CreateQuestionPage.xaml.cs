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
    public partial class CreateQuestionPage : ContentPage
    {
        public CreateQuestionPage()
        {
            InitializeComponent();
        }
        public void SetEventsAndElements()
        {
            ((CreateQuestionPageViewModel)this.BindingContext).BackToQuastionEvent += BackToQuestion;
        }
        public async void BackToQuestion()
        {
            await Navigation.PopAsync();
        }
    }
}