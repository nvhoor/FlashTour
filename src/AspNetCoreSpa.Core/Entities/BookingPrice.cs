using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class BookingPrice : IEntityBase
    {
        [Key]
        public int Id {get; set;}
        public int BookingDetailId {get; set;}
        public TourBookingDetail TourBookingDetail { get; set; }
        public int TouristTypeId {get; set;}
        public TouristType TouristType { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public decimal Price {get; set;}
    }
}