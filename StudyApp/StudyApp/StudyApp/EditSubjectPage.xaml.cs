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
    public partial class EditSubjectPage : ContentPage
    {
        public static Subject subject;

        ObservableCollection<Card> displayCards = new ObservableCollection<Card>();
        public ObservableCollection<Card> Cards { get { return displayCards; } }
        public EditSubjectPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "Cancel");

            subjectName.Text = subject.Name;

            if (subject.Cards != null)
            {
                foreach (Card card in subject.Cards)
                {
                    displayCards.Add(card);
                }
            }

            listViewSubjectCards.ItemsSource = Cards;
        }

        private void buttonAddClicked(object sender, EventArgs e)
        {
            displayCards.Add(new Card() { Question = "", Answer = "" });
        }

        private void buttonRemoveClicked(object sender, EventArgs e)
        {
            if (displayCards.Count == 0)
            {
                displayCards.Clear();
            }
            else if (displayCards.Count > 0)
            {
                displayCards.RemoveAt(displayCards.Count - 1);
            }
        }

        private async void buttonSaveClicked(object sender, EventArgs e)
        {
            Subject newSubject;

            if (listViewSubjectCards.ItemsSource != null && subjectName.Text != null)
            {
                ObservableCollection<Card> temp = new ObservableCollection<Card>();

                foreach (Card card in listViewSubjectCards.ItemsSource)
                {
                    temp.Add(card);
                }

                Card[] saveCards = new Card[temp.Count];

                for (int i = 0; i < temp.Count; i++)
                {
                    saveCards[i] = temp[i];
                }

                newSubject = new Subject { Id = subject.Id, Name = subjectName.Text, Cards = saveCards };

                if (newSubject.Name != subject.Name || newSubject.Cards != subject.Cards)
                {
                    await App.dataManager.UpdateSubjectAsync(newSubject);
                }
                else
                {
                    await DisplayAlert("Warning", "No changes were made to " + subjectName.Text, "OK");
                }
                await Navigation.PushAsync(new MainPage());
            }
            else if (listViewSubjectCards.ItemsSource == null && subjectName.Text != null)
            {
                newSubject = new Subject { Id = subject.Id, Name = subjectName.Text };

                if (newSubject.Name != subject.Name || newSubject.Cards != subject.Cards)
                {
                    await App.dataManager.UpdateSubjectAsync(newSubject);
                }
                else
                {
                    await DisplayAlert("Warning", "No changes were made to " + subjectName.Text, "OK");
                }
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Warning", "Please name this subject", "OK");
            }
        }

        private async void buttonCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void buttonDeleteClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning", "Are you sure you want to delete " + subjectName.Text + "?", "Yes", "No");

            if (answer)
            {
                await App.dataManager.DeleteSubjectAsync(subject.Id);
                await Navigation.PushAsync(new MainPage());
            }
        }
    }
}