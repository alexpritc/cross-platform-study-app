using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;

namespace StudyApp
{
    public class StoreData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string FileName { get; set; }

        // Default constructor.
        public StoreData(){}

        // Deserialise an XML file to a new instance of this type.
        public List<Subject> LoadSubjectsFromFile()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(FileName);

                XmlNodeList subjectNodeList = xmlDoc.DocumentElement.SelectNodes("/Data/Subject");

                string sID = "", sName = "";
                ObservableCollection<Card> sCards = new ObservableCollection<Card>();

                List<Subject> subjects = new List<Subject>();

                foreach (XmlNode subjectNode in subjectNodeList)
                {
                    sID = subjectNode.SelectSingleNode("Subject_ID").InnerText;
                    sName = subjectNode.SelectSingleNode("Subject_Name").InnerText;

                    if (subjectNode.SelectSingleNode("Subject_Cards").InnerText != null)
                    {
                        foreach (XmlNode cardNode in subjectNode.SelectSingleNode("Subject_Cards"))
                        {
                            string question = "";
                            string answer = "";

                            if (cardNode.Name == "Cards_Card")
                            {
                                question = cardNode.SelectSingleNode("Card_Question").InnerText;
                                answer = cardNode.SelectSingleNode("Card_Answer").InnerText;

                                sCards.Add(new Card { Answer = answer, Question = question });
                            }
                        }
                    }

                    Card[] sCardsArray;

                    if (sCards != null)
                    {
                        sCardsArray = new Card[sCards.Count];

                        for (int i = 0; i < sCards.Count; i++)
                        {
                            sCardsArray[i] = sCards[i];
                        }
                    }
                    else
                    {
                        sCardsArray = null;
                    }

                    subjects.Add(new Subject { Id = sID, Name = sName, Cards = sCardsArray });
                }

                return subjects;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // Serialise this instance to an XML file.
        public void SetFileName(string fn)
        {
            this.FileName = fn;
        }

        public void SaveSubjectsToFile(List<Subject> subjects)
        {
            XmlTextWriter writer = new XmlTextWriter(FileName, Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Data");

            if (subjects != null)
            {
                for (int i = 0; i < subjects.Count; i++)
                {
                    CreateNode(subjects[i].Id, subjects[i].Name, subjects[i].Cards, writer);
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void CreateNode(string sID, string sName, Card[] sCards, XmlTextWriter writer)
        {
            writer.WriteStartElement("Subject");
            writer.WriteStartElement("Subject_ID");
            writer.WriteString(sID);
            writer.WriteEndElement();
            writer.WriteStartElement("Subject_Name");
            writer.WriteString(sName);
            writer.WriteEndElement();
            writer.WriteStartElement("Subject_Cards");

            if (sCards != null)
            {
                foreach (Card card in sCards)
                {
                    writer.WriteStartElement("Cards_Card");
                    writer.WriteStartElement("Card_Question");
                    writer.WriteString(card.Question);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Card_Answer");
                    writer.WriteString(card.Answer);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}
