using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class BookingPrice : IEntityBase
    {
        [Key]
        public Guid Id {get; set;}
        public int BookingDetailId {get; set;}
        public TourBookingDetail TourBookingDetail { get; set; }
        public Guid TouristTypeId {get; set;}
        public TouristType TouristType { get; set; }
        public decimal Price {get; set;}
    }
}