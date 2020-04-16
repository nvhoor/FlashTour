using System;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class SearchTourVM
    {
        public DateTime DepartureDate { get; set; }
        public string DepartureId { get; set; }
        public string DestinationId { get; set; }
        public string TourCategoryId { get; set; }
        public int Price { get; set; }
    }
}