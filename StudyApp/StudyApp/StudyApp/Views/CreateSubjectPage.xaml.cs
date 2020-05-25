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
    public partial class CreateSubjectPage : ContentPage
    {
        ObservableCollection<Card> displayCards = new ObservableCollection<Card>();
        public ObservableCollection<Card> Cards { get { return displayCards; } }

        public CreateSubjectPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "Cancel");


            listViewNewSubjectCards.ItemsSource = Cards;
        }

        // Saves the new subject depending on the data the user has inputted.
        private async void buttonSaveClicked(object sender, EventArgs e)
        {
            Subject newSubject;

            if (listViewNewSubjectCards.ItemsSource != null && newSubjectName.Text != null)
            {
                ObservableCollection<Card> temp = new ObservableCollection<Card>();

                foreach (Card card in listViewNewSubjectCards.ItemsSource)
                {
                    temp.Add(card);
                }

                Card[] saveCards = new Card[temp.Count];

                for (int i = 0; i < temp.Count; i++)
                {
                    saveCards[i] = temp[i];
                }

                newSubject = new Subject { Id = generateRandomID(), Name = newSubjectName.Text, Cards = saveCards};

                await BaseViewModel.dataManager.AddSubjectAsync(newSubject);
                await Navigation.PushAsync(new MainPage());
            }
            else if (listViewNewSubjectCards.ItemsSource == null && newSubjectName.Text != null)
            {
                newSubject = new Subject { Id = generateRandomID(), Name = newSubjectName.Text};

                await BaseViewModel.dataManager.AddSubjectAsync(newSubject);
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Warning", "Please name this subject", "OK");
            }
        }

        // Sends the user back to the main page.
        private async void buttonCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        // Adds a new card.
        private void buttonAddClicked(object sender, EventArgs e)
        {
            displayCards.Add(new Card() { Question = "", Answer = "" });
        }

        // Removes cards from the subject.
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

        // Generates a random id for the subject
        private string generateRandomID()
        {
            int length = 4;

            // Creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            return str_build.ToString();
        }
    }
}