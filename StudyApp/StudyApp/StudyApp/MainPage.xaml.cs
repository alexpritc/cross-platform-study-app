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

            List<Subject> subjects = new List<Subject>();
            subjects.Add(new Subject() { Name = "Computer Science"  });
            subjects.Add(new Subject() { Name = "English" });
            subjects.Add(new Subject() { Name = "Psychology" });

            listViewSubjects.ItemsSource = subjects;
        }
    }
}
