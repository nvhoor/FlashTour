using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class BookingPrice : IEntityBase
    {
        [Key]
        public Guid Id {get; set;}
        public Guid TourBookingId {get; set;}
        public TourBooking TourBooking { get; set; }
        public int TouristTypeId {get; set;}
        public TouristType TouristType { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public decimal Price {get; set;}
    }
}