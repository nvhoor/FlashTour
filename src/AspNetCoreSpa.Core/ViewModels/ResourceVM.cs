using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class ResourceVM
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public virtual Culture Culture { get; set; }
    }
}