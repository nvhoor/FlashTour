using System.Collections.Generic;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<TourViewModel> Tours { get; set; }
    }
}