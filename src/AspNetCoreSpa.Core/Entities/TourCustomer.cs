using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class TourCustomer : IEntityBase
    {
        [Key]
        public int Id {get; set;}
        [Column(TypeName="NVARCHAR(100)")]
        public String FullName {get; set;}
        public Gender Gender {get;set;}
        public DateTime BirthDay {get; set;}
        public int TourBookingId {get;set;}  
        public int TouristTypeId {get;set;}  
        public TouristType TouristType { get; set; }
    }
}