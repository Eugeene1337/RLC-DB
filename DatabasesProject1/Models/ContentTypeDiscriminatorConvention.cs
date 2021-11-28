using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;
using System;

namespace DatabasesProject1.Models
{
    public class ContentTypeDiscriminatorConvention : IDiscriminatorConvention
    {
        public string ElementName
        {
            get { return "_materialType"; }
        }

        public Type GetActualType(IBsonReader bsonReader, Type nominalType)
        {
            var bookmark = bsonReader.GetBookmark();
            bsonReader.ReadStartDocument();
            string typeValue = string.Empty;
            if (bsonReader.FindElement(ElementName))
                typeValue = bsonReader.ReadString();
            else
                throw new NotSupportedException();

            bsonReader.ReturnToBookmark(bookmark);
            return Type.GetType(typeValue);
        }

        public MongoDB.Bson.BsonValue GetDiscriminator(Type nominalType, Type actualType)
        {
            return actualType.Name;
        }
    }
}
