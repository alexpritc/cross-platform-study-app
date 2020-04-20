using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace StudyApp
{
    public class Info
    {
        public int TimesTested { get; set; }
        public ObservableCollection<double> Percentages { get; set; }
    }
}
