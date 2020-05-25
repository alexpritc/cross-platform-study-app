using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudyApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MainPageViewModel();

            buttonCreate.Clicked += buttonCreateClicked;

            listViewSubjects.ItemsSource = viewModel.Subjects;
        }

        // Load new page for creating a subject.
        private async void buttonCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateSubjectPage());
        }

        // Loads a new page for testing the selected subject.
        private async void buttonTestClicked(object sender, EventArgs e)
        {
            if (listViewSubjects.SelectedItem == null)
            {
                await DisplayAlert("Warning", "Please select a subject to test yourself on", "OK");
            }
            else
            {
                Subject temp = listViewSubjects.SelectedItem as Subject;

                if (temp.Cards == null)
                {
                    await DisplayAlert("Warning", "This subject has no cards, you can add cards by editing the subject", "OK");
                }
                else
                {
                    TestPage.subject = temp;
                    await Navigation.PushAsync(new TestPage());
                }
            }     
        }

        // Loads a new page for practising the selected subject.
        private async void buttonPractiseClicked(object sender, EventArgs e)
        {
            if (listViewSubjects.SelectedItem == null)
            {
                await DisplayAlert("Warning", "Please select a subject to practise", "OK");
            }
            else
            {
                Subject temp = listViewSubjects.SelectedItem as Subject;

                if (temp.Cards == null)
                {
                    await DisplayAlert("Warning", "This subject has no cards, you can add cards by editing the subject", "OK");
                }
                else
                {
                    PractisePage.subject = temp;
                    await Navigation.PushAsync(new PractisePage());
                }
            }
        }

        // Edits the selected subject.
        private async void buttonEditClicked(object sender, EventArgs e)
        {
            if (listViewSubjects.SelectedItem == null)
            {
                await DisplayAlert("Warning", "Please select a subject to edit", "OK");
            }
            else
            {
                EditSubjectPage.subject = listViewSubjects.SelectedItem as Subject;

                await Navigation.PushAsync(new EditSubjectPage());
            }
        }
    }
}
