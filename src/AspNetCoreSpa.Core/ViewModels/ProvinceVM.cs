using System;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class ProvinceVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }  
    }
}