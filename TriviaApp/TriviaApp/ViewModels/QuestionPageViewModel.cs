using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaApp.Models;
using TriviaApp.Services;
using TriviaApp.Views;
using Xamarin.Forms;

namespace TriviaApp.ViewModels
{
    class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
    class QuestionPageViewModel : INotifyPropertyChanged
    {
        private TriviaAppWebApiProxy proxy;
        private User currentUser;
        public AmericanQuestion Question { get; set; }
        public Answer[] Answers { get; set; }
        private string answer1;
        public string Answer1
        {
            get
            {
                return answer1;
            }
            set
            {
                if(answer1 != value)
                {
                    answer1 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string answer2;
        public string Answer2
        {
            get
            {
                return answer2;
            }
            set
            {
                if (answer2 != value)
                {
                    answer2 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string answer3;
        public string Answer3
        {
            get
            {
                return answer3;
            }
            set
            {
                if (answer3 != value)
                {
                    answer3 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string answer4;
        public string Answer4
        {
            get
            {
                return answer4;
            }
            set
            {
                if (answer4 != value)
                {
                    answer4 = value;
                    OnPropertyChanged();
                }
            }
        }
        private string questionText;
        public string QuestionText
        {
            get
            {
                return questionText;
            }
            set
            {
                if (questionText != value)
                {
                    questionText = value;
                    OnPropertyChanged();
                }
            }
        }
        private int counter;
        public int Counter
        {
            get
            {
                return counter;
            }
            set
            {
                if(counter != value)
                {
                    counter = value;
                    OnPropertyChanged();
                    this.CounterText = $"You Answered {counter} corrects answers in a row";
                }
            }
        }
        private string counterText;
        public string CounterText
        {
            get
            {
                return counterText;
            }
            set
            {
                if(this.counterText != value)
                {
                    this.counterText = value;
                    OnPropertyChanged();
                }
            }
        }
        private int correctAnswerNum;
        public int CorrectAnswerNum
        {
            get
            {
                return correctAnswerNum;
            }
            set
            {
                if(correctAnswerNum != value)
                {
                    correctAnswerNum = value;
                    OnPropertyChanged();
                }
            }
        }
        private string result;
        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                if(result != value)
                {
                    result = value;
                    OnPropertyChanged();
                }
            }
        }
        private string btnText;
        public string BtnText
        {
            get
            {
                return btnText;
            }
            set
            {
                if(btnText != value)
                {
                    btnText = value;
                    OnPropertyChanged();
                }
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public QuestionPageViewModel()
        {

        }
        public static async Task<QuestionPageViewModel> CreateQuestionPageViewModel(User u)
        {
            QuestionPageViewModel vm = new QuestionPageViewModel();
            vm.currentUser = u;
            vm.Counter = 0;
            vm.CounterText = $"You Answered {vm.Counter} corrects answers in a row";
            vm.proxy = TriviaAppWebApiProxy.CreateProxy();
            vm.Question = await vm.proxy.GetRandomQuestionAsync();
            vm.Answers = new Answer[4];
            vm.MatchAnswers();
            vm.BtnText = "Next Question";
            return vm;
        }
        public async void ResetQuestion()
        {
            proxy = TriviaAppWebApiProxy.CreateProxy();
            Question = await proxy.GetRandomQuestionAsync();
            Answers = new Answer[4];
            MatchAnswers();
            BtnText = "Next Question";
        }
        private void MatchAnswers()
        {
            QuestionText = $"{Question.QText}\nUploaded By {Question.CreatorNickName}";
            Result = "";
            Random r = new Random();
            int CorrectAnswer = r.Next(0, Answers.Length);
            Answers[CorrectAnswer] = new Answer 
            { 
                Text = Question.CorrectAnswer, 
                IsCorrect = true 
            };
            int InCorrectAnswer1 = r.Next(0, Answers.Length);
            while(InCorrectAnswer1 == CorrectAnswer)
                InCorrectAnswer1 = r.Next(0, Answers.Length);
            Answers[InCorrectAnswer1] = new Answer 
            { 
                Text = Question.OtherAnswers[0], 
                IsCorrect = false 
            };
            int InCorrectAnswer2 = r.Next(0, Answers.Length);
            while(InCorrectAnswer2 == CorrectAnswer || InCorrectAnswer2 == InCorrectAnswer1)
                InCorrectAnswer2 = r.Next(0, Answers.Length);
            Answers[InCorrectAnswer2] = new Answer 
            { 
                Text = Question.OtherAnswers[1], 
                IsCorrect = false 
            };
            int InCorrectAnswer3 = 0;
            for (int i = 1; i < Answers.Length; i++)
            {
                if (i != CorrectAnswer && i != InCorrectAnswer1 && i != InCorrectAnswer2)
                    InCorrectAnswer3 = i;
            }
            Answers[InCorrectAnswer3] = new Answer 
            { 
                Text = Question.OtherAnswers[2], 
                IsCorrect = false 
            };
            correctAnswerNum = CorrectAnswer;
            Answer1 = Answers[0].Text;
            Answer2 = Answers[1].Text;
            Answer3 = Answers[2].Text;
            Answer4 = Answers[3].Text;
        }
        public Action NextQuestionEvent;
        public Action<Page> MoveToCreateQuestionEvent;
        public Action<int, int> InCorrectChosenEvent;
        public Action<int> CorrectChosenEvent;
        public ICommand NextButtonClickedCommand => new Command(NextButtonClicked);

        private void NextButtonClicked()
        {
            if(Counter == 3)
            {
                MoveToCreateQuestion();
            }
            else
            {
                NextQuestion();
            }
        }

        public ICommand AnswerChosenCommand => new Command<string>(AnswerChosen);
        private void AnswerChosen(string objStr)
        {
            int obj = 0;
            if (objStr == "1")
            {
                obj = 1;
            }
            if (objStr == "2")
            {
                obj = 2;
            }
            if (objStr == "3")
            {
                obj = 3;
            }
            if (Answers[obj].IsCorrect)
            {
                CorrectChosenEvent(obj);
                Counter++;
                if(Counter == 3)
                {
                    BtnText = "Create Question";
                }
                Result = "You chose the correct answer!";
            }
            else
            {
                Counter = 0;
                InCorrectChosenEvent(obj, CorrectAnswerNum);
                Result = "You chose incorrectly!";

            }
        }

      //  public ICommand NextQuestionCommand => new Command(NextQuestion);

        private void NextQuestion()
        {
            NextQuestionEvent();
        }
      //  public ICommand MoveToCreateQuestionCommand => new Command(MoveToCreateQuestion);

        private void MoveToCreateQuestion()
        {
            Counter = 0;
            CreateQuestionPage p = new CreateQuestionPage();
            p.BindingContext = new CreateQuestionPageViewModel(currentUser);
            p.SetEventsAndElements();
            MoveToCreateQuestionEvent(p);
        }
    }
}
