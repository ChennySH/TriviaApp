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
    public partial class EditQuestionPage : ContentPage
    {
        public EditQuestionPage()
        {
            InitializeComponent();
        }
        public void SetEvents()
        {
            ((EditQuestionPageViewModel)this.BindingContext).PopAction += PopPage;
        }
        public async void PopPage()
        {
            await Navigation.PopAsync();
        }
    }
}