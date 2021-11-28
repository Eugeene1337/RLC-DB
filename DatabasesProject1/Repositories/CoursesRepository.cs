using DatabasesProject1.Models;
using DatabasesProject1.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DatabasesProject1.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly IMongoCollection<Course> _courses;

        public CoursesRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _courses = database.GetCollection<Course>("Courses");
        }

        public Course Create(Course item)
        {
            _courses.InsertOne(item);
            return item;
        }

        public IList<Course> Read() =>
            _courses.Find(sub => true).ToList();

        public Course Find(string id) =>
            _courses.Find(sub => sub.CourseId == id).SingleOrDefault();

        public void Update(Course item) =>
            _courses.ReplaceOne(s => s.CourseId == item.CourseId, item);

        public void Delete(string id) =>
            _courses.DeleteOne(s => s.CourseId == id);
    }
}
