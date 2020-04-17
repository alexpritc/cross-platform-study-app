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
            List<string> subjectNames = new List<string>();

            foreach (Subject subject in subjects)
            {
                subjectNames.Add(subject.Id + ", " + subject.Name);
            }

            listViewSubjects.ItemsSource = subjectNames;
        }

        private void OnSubjectSelected(object sender, SelectedItemChangedEventArgs ctx)
        {
            var subject = ctx.SelectedItem as Subject;

            if (subject == null)
            {
                buttonDelete.IsEnabled = false;
                return;
            }
            else
            {
                buttonDelete.IsEnabled = true;
            }

            // Manually deselect item.
            listViewSubjects.SelectedItem = null;
        }

        private async void buttonCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateSubjectPage());
        }

        private void buttonDeleteClicked(object sender, EventArgs e)
        {

        }
    }
}
