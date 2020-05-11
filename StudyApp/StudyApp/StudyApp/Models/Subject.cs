using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace StudyApp
{
    [XmlType]
    public class Subject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Card[] Cards { get; set; }
    }
}
