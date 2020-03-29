using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourNewestVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime DepartureDate { get; set; }
        public Guid DepartureId { get; set; }
        public int Slot { get; set; }
        public decimal OriginalPrice {get;set;}
        public decimal PromotionPrice {get;set;}
        public DateTime StartDatePro {get; set;}
        public int TouristTypeId {get;set;}
    }
}