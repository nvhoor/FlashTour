using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class AccountViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Email {get; set;}
        public string Phone {get; set;}
        [DataType(DataType.Date)]
        public DateTime BirthDay {get; set;}
        public string Address {get; set;}
        public bool Deleted {get; set;}
        public int RoleId {get; set;}
        public Role Role { get; set; }
        public ICollection<TourBookingViewModel> TourBookings { get; set; }
    }
}