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
        public MainPage()
        {
            InitializeComponent();

            buttonCreate.Clicked += buttonCreateClicked;

            List<Subject> subjects = ModifyData.subjects;

            listViewSubjects.ItemsSource = subjects;
        }

        private async void buttonCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateSubjectPage());
        }

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
                    var answer = await DisplayAlert("Warning", "Completed test scores will be permanently stored. Are you sure you want to begin test?", "Yes", "No");

                    if (answer)
                    {
                        TestPage.subject = temp;
                        await Navigation.PushAsync(new TestPage());
                    }
                }
            }     
        }

        private async void buttonPractiseClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Which subject would you like to practise?", "Cancel", null, "Computer Science", "Psychology", "Facebook");

            Subject temp = App.dataManager.GetSubjectFromNameAsync(action).Result;

            if (temp.Cards == null)
            {
                await DisplayAlert("Warning", "This subject has no cards, you can add cards by editing the subject", "OK");
            }
            else
            {

            }
        }

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
