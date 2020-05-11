using System;
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
            subjects = App.storageManager.LoadSubjectsFromFile();
        }

        public async Task<bool> AddSubjectAsync(Subject newSubject)
        {
            subjects.Add(newSubject);

            Save();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateSubjectAsync(Subject subject)
        {
            var oldItem = subjects.Where((Subject arg) => arg.Id == subject.Id).FirstOrDefault();
            subjects.Remove(oldItem);
            subjects.Add(subject);

            Save();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteSubjectAsync(string id)
        {
            var oldItem = subjects.Where((Subject arg) => arg.Id == id).FirstOrDefault();
            subjects.Remove(oldItem);

            Save();

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

        private void Save()
        {
            App.storageManager.SaveSubjectsToFile(subjects);
        }
    }
}

