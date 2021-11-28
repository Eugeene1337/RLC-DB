using DatabasesProject1.Models;

namespace DatabasesProject1.ViewModels
{
    public class MaterialViewModel
    {
        public string MaterialId { get; set; }

        public string MaterialName { get; set; }

        public string Content { get; set; }

        public ContentType ContentType { get; set; }
    }
}
