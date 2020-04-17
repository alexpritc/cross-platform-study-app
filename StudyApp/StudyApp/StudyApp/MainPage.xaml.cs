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

        private void buttonDeleteClicked(object sender, EventArgs e)
        {

        }

        private async void OnSubjectTapped(object sender, ItemTappedEventArgs ctx)
        {
            EditSubjectPage.subject = listViewSubjects.SelectedItem as Subject;

            await Navigation.PushAsync(new EditSubjectPage());
        }
    }
}
