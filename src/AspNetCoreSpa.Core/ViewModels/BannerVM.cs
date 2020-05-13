using System;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class BannerVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Censorship { get; set; }
        public Guid PostId { get; set; }
    }
}