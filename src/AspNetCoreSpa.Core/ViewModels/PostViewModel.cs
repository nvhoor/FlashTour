
namespace AspNetCoreSpa.Core.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostContent { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWord { get; set; }
        public string Alias { get; set; }
        public bool Status { get; set; }
        public bool Censorship { get; set; }
        public bool Deleted { get; set; }
        public string PostCategoryName { get; set; }
    }
}