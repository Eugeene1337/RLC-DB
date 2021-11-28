using DatabasesProject1.Models;
using System.Collections.Generic;

namespace DatabasesProject1.Repositories.Interfaces
{
    public interface ILanguagesRepository
    {
        Language Create(Language item);

        IList<Language> Read();

        Language Find(string id);

        void Update(Language item);

        void Delete(string id);
    }
}
