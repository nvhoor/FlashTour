using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class TourBooking : AuditableEntity,IEntityBase{
        [Key]
        public Guid Id {get; set;}
        [Column(TypeName="NVARCHAR(100)")]
        public string FullName {get;set;}
        [EmailAddress]
        public string Email {get; set;}
        [Column(TypeName="VARCHAR(20)")]
        public string Mobile {get; set; }
        [Column(TypeName="NVARCHAR(255)")]
        public string Address {get; set;}
        [Column(TypeName="NVARCHAR(500)")]
        public string Note {get; set;}
        public Guid UserId {get;set;}
        public Account User { get; set; }
        public int Status {get;set;}
        public bool Deleted {get;set;}
        public ICollection<TourCustomer> TourCustomers { get; set; }
        public ICollection<TourBookingDetail> TourBookingDetails { get; set; }
        
    }
}