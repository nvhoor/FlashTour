using System;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class BookingPriceVM
    {
        public Guid Id {get; set;}
        public Guid TourBookingId {get; set;}
        public TourBooking TourBooking{ get; set; }
        public int TouristTypeId {get; set;}
        public TouristType TouristType { get; set; }
        public decimal Price {get; set;}
    }
}