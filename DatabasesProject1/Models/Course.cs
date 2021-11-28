using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabasesProject1.Models
{
    public enum Level
    {
        A1, A2, B1, B2, C1, С2,
    }

    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CourseId { get; set; }

        public string CourseName { get; set; }

        public Level Level { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public Language Language { get; set; }

        public IList<MaterialBase> Materials { get; set; }
    }
}
