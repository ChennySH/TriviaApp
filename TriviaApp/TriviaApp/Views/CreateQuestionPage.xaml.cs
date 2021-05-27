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
            ((CreateQuestionPageViewModel)this.BindingContext).QuestionCreatedEvent += QuestionCreated;
        }
        public void QuestionCreated()
        {
            MessegeLabel.TextColor = Color.DarkGreen;
            QuestionTextEntry.IsEnabled = false;
            IncorrectAnswer1Entry.IsEnabled = false;
            IncorrectAnswer2Entry.IsEnabled = false;
            IncorrectAnswer3Entry.IsEnabled = false;
            CreateQuestionButton.IsEnabled = false;
        }
        public async void BackToQuestion()
        {

            await Navigation.PopAsync();
        }
    }
}