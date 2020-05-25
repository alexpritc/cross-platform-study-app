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
    public partial class TestPage : ContentPage
    {
        public static Subject subject;

        // Variables for collecting stats.
        int testLength;
        int currentQuestion = 0;

        int correctAnswers = 0;

        public TestPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "Cancel");

            subjectName.Text = subject.Name;
            testLength = subject.Cards.Length;

            if (subject.Cards != null)
            {
                totalNumberOfQuestions.Text = testLength.ToString();
                currentQuestionNumber.Text = "0";

                buttonNext.Text = "Begin test!";
            }
        }

        // Takes the user back to the main page after getting confirmation.
        private async void buttonCancelClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning", "Are you sure you want to abandon this test?", "Yes", "No");

            if (answer)
            {
                await Navigation.PushAsync(new MainPage());
            }
        }

        // Displays next question.
        private async void buttonNextClicked(object sender, EventArgs e)
        {
            if (buttonNext.Text == "Begin test!")
            {
                buttonNext.Text = "Next question";
            }

            if (currentQuestion < testLength)
            {
                // Checks if answer matches the question.
                int displayQuestionNum = currentQuestion + 1;

                string question = subject.Cards[currentQuestion].Question;
                string answer = subject.Cards[currentQuestion].Answer;

                string result = await DisplayPromptAsync("Question " + displayQuestionNum, question);

                if (result.ToLower() == answer.ToLower())
                {
                    correctAnswers++;
                }

                currentQuestion++;
                currentQuestionNumber.Text = displayQuestionNum.ToString();

                // Displays results
                if (currentQuestion >= testLength)
                {
                    bool response;

                    if (correctAnswers >= testLength)
                    {
                        response = await DisplayAlert("Results", "Congratulations!!!! You scored " + correctAnswers + "/" + testLength + " on this test. " +
                        "Would you like to try again?", "Yes", "No");
                    }
                    else if (correctAnswers >= testLength / 2)
                    {
                        response = await DisplayAlert("Results", "Nice try! You scored " + correctAnswers + "/" + testLength + " on this test. " +
                        "Would you like to try again?", "Yes", "No");
                    }
                    else
                    {
                        response = await DisplayAlert("Results", "Better luck next time... You scored " + correctAnswers + "/" + testLength + " on this test. " +
                            "Would you like to try again?", "Yes", "No");
                    }

                    if (!response)
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await Navigation.PushAsync(new TestPage());
                    }
                }
            }
        }
        
    }
}