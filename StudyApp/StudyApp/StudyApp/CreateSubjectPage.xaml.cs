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

                newSubject = new Subject { Id = "0000", Name = newSubjectName.Text, Cards = saveCards};

                await App.dataManager.AddSubjectAsync(newSubject);
                await Navigation.PushAsync(new MainPage());
            }
            else if (listViewNewSubjectCards.ItemsSource == null && newSubjectName.Text != null)
            {
                newSubject = new Subject { Id = "0000", Name = newSubjectName.Text};

                await App.dataManager.AddSubjectAsync(newSubject);
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
    }
}