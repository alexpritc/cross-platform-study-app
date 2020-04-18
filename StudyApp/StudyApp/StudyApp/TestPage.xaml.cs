﻿using System;
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

        ObservableCollection<Card> displayCard = new ObservableCollection<Card>();
        public ObservableCollection<Card> Cards { get { return displayCard; } }
        public TestPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "Cancel");

            subjectName.Text = subject.Name;

            if (subject.Cards != null)
            {
                displayCard.Add(subject.Cards[counter]);
            }

            listViewSubjectCards.ItemsSource = Cards;
        }

        private async void buttonCancelClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning", "Are you sure you want to abandon this test?", "Yes", "No");

            if (answer)
            {
                await Navigation.PushAsync(new MainPage());
            }
        }

        private void buttonNextClicked(object sender, EventArgs e)
        {

        }

        private void buttonPreviousClicked(object sender, EventArgs e)
        {

        }

        private void cardTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}