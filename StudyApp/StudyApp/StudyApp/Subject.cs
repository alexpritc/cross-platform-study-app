using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp
{
    public class Subject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Card[] Cards { get; set; }

        public Info Information { get; set; }
    }
}
