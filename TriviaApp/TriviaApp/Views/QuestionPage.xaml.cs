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
    public partial class QuestionPage : ContentPage
    {
        private Button[] buttons;
        private Frame[] frames;
        public QuestionPage()
        {
            InitializeComponent();     
        }
        public void SetEventsAndElements()
        {
            ((QuestionPageViewModel)this.BindingContext).NextQuestionEvent += MoveToNextQuestion;
            ((QuestionPageViewModel)this.BindingContext).MoveToCreateQuestionEvent += MoveToCreateQuestion;
            ((QuestionPageViewModel)this.BindingContext).CorrectChosenEvent += CorrectAnswerChosen;
            ((QuestionPageViewModel)this.BindingContext).InCorrectChosenEvent += InCorrectAnswerChosen;
            InitializeComponent();
            buttons = new Button[4];
            buttons[0] = Answer1Btn;
            buttons[1] = Answer2Btn;
            buttons[2] = Answer3Btn;
            buttons[3] = Answer4Btn;
            frames = new Frame[4];
            frames[0] = Answer1Frm;
            frames[1] = Answer2Frm;
            frames[2] = Answer3Frm;
            frames[3] = Answer4Frm;
            NextPageBtn.IsEnabled = false;
        }
        public void MoveToNextQuestion()
        {
            ((QuestionPageViewModel)this.BindingContext).ResetQuestion();
        }
        public async void MoveToCreateQuestion(Page p)
        {
            await Navigation.PushAsync(p);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(this.BindingContext != null && this.BindingContext is QuestionPageViewModel)
            {
                ((QuestionPageViewModel)this.BindingContext).ResetQuestion();
            }
        }
        public void CorrectAnswerChosen(int num)
        {
            buttons[num].BackgroundColor = Color.Green;
            frames[num].BackgroundColor = Color.Green;
            ResultLabel.TextColor = Color.DarkGreen;
            NextPageBtn.IsEnabled = true;
        }
        public void InCorrectAnswerChosen(int chosen, int correct)
        {
            buttons[chosen].BackgroundColor = Color.Red;
            frames[chosen].BackgroundColor = Color.Red;
            buttons[correct].BackgroundColor = Color.Green;
            frames[correct].BackgroundColor = Color.Green;
            ResultLabel.TextColor = Color.DarkRed;
            NextPageBtn.IsEnabled = true;
        }
    }
}