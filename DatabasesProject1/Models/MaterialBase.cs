using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DatabasesProject1.Models
{
    public enum ContentType
    {
        Text, File, Video
    }

    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(TextMaterial), typeof(FileMaterial), typeof(VideoMaterial))]
    public abstract class MaterialBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MaterialId { get; set; }

        public string MaterialName { get; set; }
    }
}
