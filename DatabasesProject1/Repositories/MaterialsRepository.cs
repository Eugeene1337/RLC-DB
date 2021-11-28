using DatabasesProject1.Models;
using DatabasesProject1.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabasesProject1.Repositories
{
    public class MaterialsRepository: IMaterialsRepository
    {
        private readonly IMongoCollection<MaterialBase> _materials;
        private readonly IMongoCollection<BsonDocument> _materialsBson;

        public MaterialsRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _materials = database.GetCollection<MaterialBase>("Materials");
            _materialsBson = database.GetCollection<BsonDocument>("Materials");
        }

        public MaterialBase Create(MaterialBase item)
        {
            _materials.InsertOne(item);
            return item;
        }

        public IList<MaterialBase> Read() =>
            _materials.Find(sub => true).ToList();

        public IList<TextMaterial> ReadTextMaterials()
        {
            List<TextMaterial> texts = new List<TextMaterial>();
            FilterDefinition<BsonDocument> filter = new BsonDocument("_t", "TextMaterial");
            var list = _materialsBson.Find(filter).ToList();

            foreach(var item in list)
            {
                TextMaterial mat = new TextMaterial();

                mat.MaterialId = item["_id"].ToString();
                mat.MaterialName = item["MaterialName"].ToString();
                mat.Content = item["Content"].ToString();

                texts.Add(mat);
            }

            return texts;
        }

        public IList<VideoMaterial> ReadVideoMaterials()
        {
            List<VideoMaterial> videos = new List<VideoMaterial>();
            FilterDefinition<BsonDocument> filter = new BsonDocument("_t", "VideoMaterial");
            var list = _materialsBson.Find(filter).ToList();

            foreach (var item in list)
            {
                VideoMaterial mat = new VideoMaterial();

                mat.MaterialId = item["_id"].ToString();
                mat.MaterialName = item["MaterialName"].ToString();
                mat.VideoUrl = item["VideoUrl"].ToString();

                videos.Add(mat);
            }

            return videos;
        }

        public IList<FileMaterial> ReadFileMaterials()
        {
            List<FileMaterial> files = new List<FileMaterial>();
            FilterDefinition<BsonDocument> filter = new BsonDocument("_t", "FileMaterial");
            var list = _materialsBson.Find(filter).ToList();

            foreach (var item in list)
            {
                FileMaterial mat = new FileMaterial();

                mat.MaterialId = item["_id"].ToString();
                mat.MaterialName = item["MaterialName"].ToString();
                mat.FileUrl = item["VideoUrl"].ToString();

                files.Add(mat);
            }

            return files;
        }

        public MaterialBase Find(string id) =>
            _materials.Find(m => m.MaterialId == id).SingleOrDefault();

        public void Update(MaterialBase item) =>
            _materials.ReplaceOne(m => m.MaterialId == item.MaterialId, item);

        public void Delete(string id) =>
            _materials.DeleteOne(m => m.MaterialId == id);
    }
}
