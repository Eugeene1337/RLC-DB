using DatabasesProject1.Models;
using DatabasesProject1.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DatabasesProject1.Repositories
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly IMongoCollection<Language> _languages;

        public LanguagesRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _languages = database.GetCollection<Language>("Languages");
        }

        public Language Create(Language item)
        {
            _languages.InsertOne(item);
            return item;
        }

        public IList<Language> Read() =>
            _languages.Find(sub => true).ToList();

        public Language Find(string id) =>
            _languages.Find(sub => sub.LanguageId == id).SingleOrDefault();

        public void Update(Language item) =>
            _languages.ReplaceOne(s => s.LanguageId == item.LanguageId, item);

        public void Delete(string id) =>
            _languages.DeleteOne(s => s.LanguageId == id);
    }
}
