using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace StudyApp
{
    class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<Subject> Subjects { get; set; }
        public Command LoadItemsCommand { get; set; }

        public MainPageViewModel()
        {
            Title = "All Subjects";
            Subjects = new ObservableCollection<Subject>();

            Load();

            MessagingCenter.Subscribe<MainPage, Subject>(this, "AddSubject", async (obj, subject) =>
            {
                var newsubject = subject as Subject;
                Subjects.Add(newsubject);
                await dataManager.AddSubjectAsync(newsubject);
            });
        }

        async Task ExecuteLoadSubjectsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Subjects.Clear();
                var subjects = await dataManager.GetSubjectsAsync(true);

                foreach (var subject in subjects)
                {
                    Subjects.Add(subject);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void Load()
        {
            await ExecuteLoadSubjectsCommand();
        }
    }
}
