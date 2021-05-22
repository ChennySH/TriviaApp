using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TriviaApp.Models;
using TriviaApp.Services;

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
        public AmericanQuestion Question { get; set; }
        public Answer[] Answers { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public QuestionPageViewModel()
        {
            proxy = TriviaAppWebApiProxy.CreateProxy();
            GetQuestion(); 
            Answers = new Answer[4];
            MatchAnswers();
        }
        private async void GetQuestion()
        {
            Question = await this.proxy.GetRandomQuestionAsync();
        }
        private void MatchAnswers()
        {
            Random r = new Random();
            int CorrectAnswer = r.Next(0, Answers.Length);
            Answers[CorrectAnswer] = new Answer { Text = Question.CorrectAnswer, IsCorrect = true };
            int InCorrectAnswer1 = r.Next(0, Answers.Length);
            while(InCorrectAnswer1 == CorrectAnswer)
                InCorrectAnswer1 = r.Next(0, Answers.Length);
            Answers[InCorrectAnswer1] = new Answer { Text = Question.OtherAnswers[0], IsCorrect = false };
            int InCorrectAnswer2 = r.Next(0, Answers.Length);
            while(InCorrectAnswer2 == CorrectAnswer || InCorrectAnswer2 == InCorrectAnswer1)
                InCorrectAnswer2 = r.Next(0, Answers.Length);
            Answers[InCorrectAnswer2] = new Answer { Text = Question.OtherAnswers[1], IsCorrect = false };
            int InCorrectAnswer3 = 0;
            for (int i = 1; i < Answers.Length; i++)
            {
                if (i != CorrectAnswer && i != InCorrectAnswer1 && i != InCorrectAnswer2)
                    InCorrectAnswer3 = i;
            }
            Answers[InCorrectAnswer3] = new Answer { Text = Question.OtherAnswers[2], IsCorrect = false };
        }
    }
}
