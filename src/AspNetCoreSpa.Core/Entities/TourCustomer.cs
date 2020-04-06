using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class TourCustomer : IEntityBase
    {
        [Key]
        public Guid Id {get; set;}
        [Column(TypeName="NVARCHAR(100)")]
        public string FullName {get; set;}
        public Gender Gender {get;set;}
        public DateTime BirthDay {get; set;}
        public Guid TourBookingId {get;set;}
        public  TourBooking TourBooking { get; set; }
        public int TouristType {get;set;} 
    }
}