using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TouristTypeVM
    {
        public Guid Id {get; set;}
        public String Name {get; set;}

        public ICollection<TourCustomer> TourCustomer { get; set; }
        public ICollection<Price> Price { get; set; }
    }
}