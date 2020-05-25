using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp
{
    // This code is taken from the practical repo on GitHub.
    public interface IData<T>
    {
        Task<bool> AddSubjectAsync(T subject);
        Task<bool> UpdateSubjectAsync(T subject);
        Task<bool> DeleteSubjectAsync(string id);
        Task<T> GetSubjectAsync(string id);
        Task<T> GetSubjectFromNameAsync(string name);
        Task<IEnumerable<T>> GetSubjectsAsync(bool forceRefresh = false);
    }
}
