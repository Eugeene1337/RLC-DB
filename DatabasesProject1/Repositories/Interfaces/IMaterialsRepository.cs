using DatabasesProject1.Models;
using System.Collections.Generic;

namespace DatabasesProject1.Repositories.Interfaces
{
    public interface IMaterialsRepository
    {
        MaterialBase Create(MaterialBase item);

        IList<MaterialBase> Read();

        IList<TextMaterial> ReadTextMaterials();
        IList<VideoMaterial> ReadVideoMaterials();
        IList<FileMaterial> ReadFileMaterials();

        MaterialBase Find(string id);

        void Update(MaterialBase item);

        void Delete(string id);
    }
}
