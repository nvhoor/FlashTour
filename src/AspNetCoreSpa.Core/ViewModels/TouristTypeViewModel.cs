using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TouristTypeViewModel
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public ICollection<TourCustomerViewModel> TourCustomer { get; set; }
        public ICollection<PriceViewModel> Price { get; set; }
    }
}