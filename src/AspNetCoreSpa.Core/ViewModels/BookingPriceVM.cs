using System;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class BookingPriceVM
    {
        public Guid? Id {get; set;}
        public Guid? TourBookingId {get; set;}
        public int TouristType {get; set;}
        public decimal Price {get; set;}
    }
}