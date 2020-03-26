using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSpa.Core.Entities
{
    public class TourBookingDetail : IEntityBase{
        [Key]
        public Guid Id {get; set;}
        public Guid TourId {get; set;}
        public Tour Tour {get; set;}
        public Guid TourBookingId {get; set;}
        public TourBooking TourBooking { get; set; }
        public ICollection<BookingPrice> BookingPrices { get; set; }
    }
}