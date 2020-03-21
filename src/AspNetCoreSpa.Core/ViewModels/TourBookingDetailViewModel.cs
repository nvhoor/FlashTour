using System.Collections.Generic;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourBookingDetailViewModel
    {
        public int Id {get; set;}
        public int TourId {get; set;}
        public int TourBookingId {get; set;}
        public ICollection<BookingPriceViewModel> BookingPrices { get; set; }
    }
}