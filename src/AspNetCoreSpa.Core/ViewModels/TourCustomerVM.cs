using System;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourCustomerVM
    {
        public Guid Id {get; set;}
        public String FullName {get; set;}
        public Gender Gender {get;set;}
        public DateTime BirthDay {get; set;}
        public int TourBookingId {get;set;}  
        public int TouristTypeId {get;set;}  
        public TouristType TouristType { get; set; } 
    }
}