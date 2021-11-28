using DatabasesProject1.Models;
using System.Collections.Generic;

namespace DatabasesProject1.Repositories.Interfaces
{
    public interface ICoursesRepository
    {
        Course Create(Course item);

        IList<Course> Read();

        Course Find(string id);

        void Update(Course item);

        void Delete(string id);
    }
}
