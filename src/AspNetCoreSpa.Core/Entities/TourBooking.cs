using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class TourBooking : IEntityBase{
        [Key]
        public int Id {get; set;}
        [Column(TypeName="NVARCHAR(100)")]
        public int FullName {get;set;}
        [EmailAddress]
        public string Email {get; set;}
        [Column(TypeName="VARCHAR(20)")]
        public String Mobile {get; set; }
        [Column(TypeName="NVARCHAR(255)")]
        public String Address {get; set;}
        [Column(TypeName="NVARCHAR(500)")]
        public String Note {get; set;}
        public int AccountId {get;set;}

        public int TourId {get;set;}

        public int StatusId {get;set;}

        public bool Deleted {get;set;}

        public ICollection<TourCustomer> TourCustomers { get; set; }
        public ICollection<TourBookingDetail> TourBookingDetails { get; set; }
    }
}