using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PractisePage : ContentPage
    {
        public static Subject subject;

        int testLength = 0;

        int prev = -1;
        int current = 0;

        public PractisePage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "Cancel");

            subjectName.Text = subject.Name;
            testLength = subject.Cards.Length;

            if (subject.Cards != null)
            {
                cardDetails.Text = subject.Cards[current].Question;
            }
        }

        // Sends the user back to the main page.
        private async void buttonCancelClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning", "Are you sure you want to stop practising?", "Yes", "No");

            if (answer)
            {
                await Navigation.PushAsync(new MainPage());
            }
        }

        // Next flash card is displayed.
        private void buttonNextClicked(object sender, EventArgs e)
        {
            if (current >= subject.Cards.Length - 1)
            {
                prev = -1;
                current = 0;
            }
            else
            {
                prev = current;
                current++;
            }

            cardDetails.Text = subject.Cards[current].Question;

            buttonReveal.Text = "Reveal Answer";
                
        }

        // Previous flash card is displayed.
        private async void buttonPreviousClicked(object sender, EventArgs e)
        {
            buttonReveal.Text = "Reveal Answer";

            if (prev != -1)
            {
                current--;
                prev--;

                cardDetails.Text = subject.Cards[current].Question;
            }
            else
            {
                await DisplayAlert("Warning", "There is no previous card", "OK");
            }
        }

        private void buttonRevealClicked(object sender, EventArgs e)
        {
            if (buttonReveal.Text == "Reveal Answer")
            {
                buttonReveal.Text = "Hide Answer";

                cardDetails.Text = subject.Cards[current].Answer;
            }
            else
            {
                buttonReveal.Text = "Reveal Answer";

                cardDetails.Text = subject.Cards[current].Question;
            }
        }
    }
}