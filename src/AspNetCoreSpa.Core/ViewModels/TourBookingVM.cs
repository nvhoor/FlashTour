using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourBookingVM
    {
        public Guid Id {get; set;}
        public int FullName {get;set;}
        [EmailAddress]
        public string Email {get; set;}
        public string Mobile {get; set; }
        public string Address {get; set;}
        public string Note {get; set;}
        public int UserId {get;set;}
        public Account User { get; set; }
        public int TourId {get;set;}
        public int StatusId {get;set;}
        public bool Deleted {get;set;}
        public ICollection<TourCustomer> TourCustomers { get; set; }
        public ICollection<TourBookingDetail> TourBookingDetails { get; set; } 
    }
}