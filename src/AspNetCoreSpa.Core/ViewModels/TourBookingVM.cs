using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourBookingVM
    {
        public Guid Id {get; set;}
        public string FullName {get;set;}
        [EmailAddress]
        public string Email {get; set;}
        public string Mobile {get; set; }
        public string Address {get; set;}
        public string Note {get; set;}
        public string UserId {get;set;}
        public Account User { get; set; }
        public Guid TourId {get;set;}
        public bool Status {get;set;}
        public bool Deleted {get;set;}
        public ICollection<TourCustomerVM> TourCustomers { get; set; }
        public ICollection<BookingPriceVM> BookingPrices { get; set; }
    }
}