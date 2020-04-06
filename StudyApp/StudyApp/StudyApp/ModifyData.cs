using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyApp
{
    class ModifyData
    {
        readonly List<Subject> subjects;

        public ModifyData()
        {
            subjects = new List<Subject>()
            {
                new Subject { Id = Guid.NewGuid().ToString(), Name = "Comp Sci" },
            };
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

        public async Task<IEnumerable<Subject>> GetSubjectsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(subjects);
        }
    }
}

