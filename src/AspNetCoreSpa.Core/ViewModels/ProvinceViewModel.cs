using System.Collections.Generic;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class ProvinceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public ICollection<TourViewModel> Tours { get; set; }
        public ICollection<TourProgramViewModel> TourPrograms { get; set; }
    }
}