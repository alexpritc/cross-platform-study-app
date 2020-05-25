using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace StudyApp
{
    [XmlType]
    // A subject has a name, id and array of flash cards.
    public class Subject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Card[] Cards { get; set; }
    }
}
