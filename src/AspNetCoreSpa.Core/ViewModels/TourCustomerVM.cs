using System;
using System.ComponentModel.DataAnnotations;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourCustomerVM
    {
        public Guid Id {get; set;}
        public string FullName {get; set;}
        public Gender Gender {get;set;}
        public DateTime BirthDay {get; set;}
        public Guid TourBookingId {get;set;}  
        public int TouristType {get;set;}    
    }
}