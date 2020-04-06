using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyApp
{
    public class Data
    {
        public interface IData<T>
        {
            Task<bool> AddSubjectAsync(T subject);
            Task<bool> UpdateSubjectAsync(T subject);
            Task<bool> DeleteSubjectAsync(string id);
            Task<T> GetSubjectAsync(string id);
            Task<IEnumerable<T>> GetSubjectsAsync(bool forceRefresh = false);
        }
    }

}
