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
        ObservableCollection<Card> cards = new ObservableCollection<Card>();

        public CreateSubjectPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "Cancel");

            listViewNewSubjectCards.ItemsSource = cards;
        }

        private async void buttonSaveClicked(object sender, EventArgs e)
        {
            Subject newSubject = new Subject { Id = "0000", Name = newSubjectName.Text };
            await Navigation.PushAsync(new MainPage());
        }

        private async void buttonCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void buttonAddClicked(object sender, EventArgs e)
        {
            cards.Add(new Card() { Question = "", Answer = "" });
        }

        private void buttonRemoveClicked(object sender, EventArgs e)
        {
            cards.RemoveAt(cards.Count - 1);
        }
    }
}