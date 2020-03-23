using System.Collections.Generic;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourBookingDetailVM
    {
        public int Id {get; set;}
        public int TourId {get; set;}
        public int TourBookingId {get; set;}
        public ICollection<BookingPriceVM> BookingPrices { get; set; }
    }
}