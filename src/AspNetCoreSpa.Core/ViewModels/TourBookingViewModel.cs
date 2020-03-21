using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourBookingViewModel
    {
        public int Id {get; set;}
        public string FullName {get;set;}
        public string Email {get; set;}
        public string Mobile {get; set; }
        public string Address {get; set;}
        public string Note {get; set;}
        public int UserId {get;set;}
        public Account User { get; set; }
        public int TourId {get;set;}
        public string TourName {get;set;}
        public bool Status {get;set;}
        public bool Deleted {get;set;}
        public ICollection<TourCustomerViewModel> TourCustomers { get; set; }
        public ICollection<TourBookingDetailViewModel> TourBookingDetails { get; set; }
    }
}