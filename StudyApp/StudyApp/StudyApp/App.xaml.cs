using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyApp
{
    public partial class App : Application
    {
        public static ModifyData dataManager = new ModifyData();

        public App()
        {
            InitializeComponent();

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
