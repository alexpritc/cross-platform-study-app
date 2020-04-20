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
        public static int counter = 0;

        int testLength;
        int currentQuestion = 0;

        int correctAnswers = 0;
        int incorrectAnswers = 0;

        ObservableCollection<string> displayInfo = new ObservableCollection<string>();
        public ObservableCollection<string> Info { get { return displayInfo; } }
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

            CalculateInfo();

            listViewInfo.ItemsSource = Info;
        }

        private async void buttonCancelClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning", "Are you sure you want to abandon this test?", "Yes", "No");

            if (answer)
            {
                await Navigation.PushAsync(new MainPage());
            }
        }

        private async void buttonNextClicked(object sender, EventArgs e)
        {
            if (buttonNext.Text == "Begin test!")
            {
                buttonNext.Text = "Next question";
            }

            if (currentQuestion < testLength)
            {
                int displayQuestionNum = currentQuestion + 1;

                string question = subject.Cards[currentQuestion].Question;
                string answer = subject.Cards[currentQuestion].Answer;

                string result = await DisplayPromptAsync("Question " + displayQuestionNum, question);

                if (result.ToLower() == answer.ToLower())
                {
                    correctAnswers++;
                }
                else
                {
                    incorrectAnswers++;
                }

                currentQuestion++;
                currentQuestionNumber.Text = displayQuestionNum.ToString();

                CalculateInfo();

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

        void CalculateInfo()
        {
            displayInfo.Clear();

            float percentage = correctAnswers / testLength * 100;
            double displayPercentage = Math.Round(percentage, 2);
            int questionsLeft = testLength - currentQuestion;

            displayInfo.Add("Percentage: " + displayPercentage.ToString());
            displayInfo.Add("Correct Answers: " + correctAnswers.ToString());
            displayInfo.Add("Incorrect Answers: " + incorrectAnswers.ToString());
            displayInfo.Add("Number of questions left: " + questionsLeft.ToString());
        }
    }
}