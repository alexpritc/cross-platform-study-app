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

            ObservableCollection<Card> temp = new ObservableCollection<Card>();

            if (subjectName.Text == "")
            {
                await DisplayAlert("Warning", "Please name this subject", "OK");
            }
            else
            {
                if (listViewSubjectCards.ItemsSource != null)
                {
                    foreach (Card card in listViewSubjectCards.ItemsSource)
                    {
                        temp.Add(card);
                    }

                    int counter = 0;
                    bool isNull = true;

                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (temp[i] != null && (temp[i].Question != "" && temp[i].Answer !=""))
                        {
                            counter++;
                            isNull = false;
                        }
                    }

                    Card[] saveCards = new Card[counter];
                    int j = 0;

                    if (isNull)
                    {
                        saveCards = null;
                    }
                    else
                    {
                        foreach (Card item in temp)
                        {
                            if (item != null && (item.Question != "" && item.Answer != ""))
                            {
                                saveCards[j] = item;
                                j++;
                            }
                        }
                    }

                    if (subject.Cards != null)
                    {
                        if (!AreCardsTheSame(subject.Cards, saveCards))
                        {
                            newSubject = new Subject { Id = subject.Id, Name = subjectName.Text, Cards = saveCards };

                            await BaseViewModel.dataManager.UpdateSubjectAsync(newSubject);
                        }
                        else
                        {
                            if (subjectName.Text == subject.Name)
                            {
                                await DisplayAlert("Warning", "No changes were made to " + subjectName.Text, "OK");
                            }
                            else
                            {
                                newSubject = new Subject { Id = subject.Id, Name = subjectName.Text, Cards = saveCards };

                                await BaseViewModel.dataManager.UpdateSubjectAsync(newSubject);
                            }
                        }
                    }
                    else
                    {
                        if (subjectName.Text == subject.Name && subject.Cards == saveCards)
                        {
                            await DisplayAlert("Warning", "No changes were made to " + subjectName.Text, "OK");
                        }
                        else
                        {
                            newSubject = new Subject { Id = subject.Id, Name = subjectName.Text, Cards = saveCards };

                            await BaseViewModel.dataManager.UpdateSubjectAsync(newSubject);
                        }
                    }
                }
                else
                {
                    if (subject.Cards != null)
                    {
                        newSubject = new Subject { Id = subject.Id, Name = subjectName.Text };

                        await BaseViewModel.dataManager.UpdateSubjectAsync(newSubject);
                    }
                    else
                    {
                        if (subjectName.Text == subject.Name)
                        {
                            await DisplayAlert("Warning", "No changes were made to " + subjectName.Text, "OK");
                        }
                        else
                        {
                            newSubject = new Subject { Id = subject.Id, Name = subjectName.Text };

                            await BaseViewModel.dataManager.UpdateSubjectAsync(newSubject);
                        }
                    }
                }

                await Navigation.PushAsync(new MainPage());
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
                await BaseViewModel.dataManager.DeleteSubjectAsync(subject.Id);
                await Navigation.PushAsync(new MainPage());
            }
        }

        private bool AreCardsTheSame(Card[] original, Card[] newCards)
        {
            bool areTheSame = true;

            if (original.Length != newCards.Length)
            {
                areTheSame = false;
            }
            else
            {
                int counter = 0;

                foreach (Card item in original)
                {
                    if (item.Question != newCards[counter].Question 
                        || item.Answer !=  newCards[counter].Answer)
                    {
                        areTheSame = false;
                    }

                    counter++;
                }
            }

            return areTheSame;
        }
    }
}