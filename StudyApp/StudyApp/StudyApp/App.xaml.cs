using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Essentials;
using System.Xml;
using System.Collections.Generic;

namespace StudyApp
{
    public partial class App : Application
    {
        public static StoreData storageManager;

        public App()
        {
            InitializeComponent();

            // Try and load from persistent storage.
            string mainDir = FileSystem.AppDataDirectory;
            string path = Path.Combine(mainDir, "subjects.xml");

            XmlDocument xmlDoc = new XmlDocument();

            List<Subject> subjects = new List<Subject>();

            // Check if there is already a file.
            try
            {
                xmlDoc.Load(path);

                storageManager = new StoreData();
                storageManager.FileName = path;
                storageManager.LoadSubjectsFromFile();
            }
            catch
            {
                // Default subject.
                subjects.Add(new Subject { Id = "0000", Name = "ExampleSubject" });

                // No such file, then create a new model with defaults and save.
                storageManager = new StoreData();

                storageManager.SetFileName(path);
                storageManager.SaveSubjectsToFile(subjects);
            }

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
