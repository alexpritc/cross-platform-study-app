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

        float percentage;

        ObservableCollection<Info> displayInfo = new ObservableCollection<Info>();
        public ObservableCollection<Info> DisplayInfo { get { return displayInfo; } }

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

            if (subject.Cards != null)
            {
                
            }

            CalculateInfo();

            listViewInfo.ItemsSource = DisplayInfo;
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

                if (currentQuestion >= testLength)
                {
                    CalculateInfo();
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

                    subject.Information.TimesTested++;
                    subject.Information.Percentages.Add(percentage);

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
            percentage = correctAnswers / testLength * 100;
            double displayPercentage = Math.Round(percentage, 2);

            subject.Information.Percentages.Add(displayPercentage);
        }
    }
}