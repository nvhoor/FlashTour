using System;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class PriceVM
    {
        public Guid Id {get; set;}
        public int TourId {get;set;}
        public string Name {get; set;}
        public decimal OriginalPrice {get;set;}
        public decimal PromotionPrice {get;set;}
        public DateTime StartDatePro {get; set;}
        public Guid TouristTypeId {get;set;}
        public TouristType TouristType { get; set; } 
    }
}