using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TouristTypeVM
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public ICollection<TourCustomerVM> TourCustomer { get; set; }
        public ICollection<PriceVM> Price { get; set; }
    }
}