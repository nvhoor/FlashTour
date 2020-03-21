using System.Collections.Generic;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class PostCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PostViewModel> Posts { get; set; }
    }
}