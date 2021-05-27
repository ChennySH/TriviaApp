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
        private int counter;
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
                return BtnText;
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
        public QuestionPageViewModel(User u)
        {
            this.currentUser = u;
            this.counter = 1;
            proxy = TriviaAppWebApiProxy.CreateProxy();
            GetQuestion();
            questionText = Question.QText;
            Answers = new Answer[4];
            MatchAnswers();
            result = "";
            BtnText = "Next Question";
        }
        public void ResetQuestion()
        {
            proxy = TriviaAppWebApiProxy.CreateProxy();
            GetQuestion();
            questionText = Question.QText;
            Answers = new Answer[4];
            MatchAnswers();
            result = "";
            if(counter < 3)
                BtnText = "Next Question";
            else
                BtnText = "Create Question";
        }
        private void GetQuestion()
        {
            Task<AmericanQuestion> t = this.proxy.GetRandomQuestionAsync();
            t.Wait();
            AmericanQuestion q = t.Result;
            Question = q;
        }
        private void MatchAnswers()
        {
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
            if(counter == 3)
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
                Result = "You chose the correct answer!";
            }
            else
            {
                InCorrectChosenEvent(obj, CorrectAnswerNum);
                Result = "You chose incorrectly!";

            }
        }

      //  public ICommand NextQuestionCommand => new Command(NextQuestion);

        private void NextQuestion()
        {
            if (Counter < 3)
                Counter++;
            else
                counter = 1;
            NextQuestionEvent();

        }
      //  public ICommand MoveToCreateQuestionCommand => new Command(MoveToCreateQuestion);

        private void MoveToCreateQuestion()
        {
            CreateQuestionPage p = new CreateQuestionPage();
            p.BindingContext = new CreateQuestionPageViewModel(currentUser);
            p.SetEventsAndElements();
            MoveToCreateQuestionEvent(p);
        }
    }
}
