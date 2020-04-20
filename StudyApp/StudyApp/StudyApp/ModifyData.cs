﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp
{
    public class ModifyData : IData<Subject>
    {
        public static List<Subject> subjects = new List<Subject>();

        public ModifyData()
        {
            Card[] cards = new Card[2];
            cards[0] = new Card { Question = "What is comp sci?", Answer = "Fun" };
            cards[1] = new Card { Question = "What are computers?", Answer = "Cool" };

            Info newInfo = new Info { TimesTested = 0 };

            AddSubjectAsync(new Subject() { Id = "0001", Name = "Computer Science", Cards = cards, Information = newInfo });
            AddSubjectAsync(new Subject() { Id = "0002", Name = "Psychology", Information = newInfo });
            AddSubjectAsync(new Subject() { Id = "0003", Name = "English Language", Information = newInfo });
        }

        public async Task<bool> AddSubjectAsync(Subject newSubject)
        {
            subjects.Add(newSubject);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateSubjectAsync(Subject subject)
        {
            var oldItem = subjects.Where((Subject arg) => arg.Id == subject.Id).FirstOrDefault();
            subjects.Remove(oldItem);
            subjects.Add(subject);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteSubjectAsync(string id)
        {
            var oldItem = subjects.Where((Subject arg) => arg.Id == id).FirstOrDefault();
            subjects.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Subject> GetSubjectAsync(string id)
        {
            return await Task.FromResult(subjects.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Subject> GetSubjectFromNameAsync(string name)
        {
            return await Task.FromResult(subjects.FirstOrDefault(s => s.Name == name));
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(subjects);
        }
    }
}

