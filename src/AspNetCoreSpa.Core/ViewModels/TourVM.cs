using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Images { get; set; }
        public string Description { get; set; }
        public DateTime DepartureDate { get; set; }
        public Guid DepartureId { get; set; }
        public Guid DestinationId { get; set; }
        public string DepartureName { get; set; }
        public int Slot { get; set; }
        public long ViewCount { get; set; }
        public bool Censorship { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public decimal OriginalPrice {get;set;}
        public decimal PromotionPrice {get;set;}
        public DateTime StartDatePro {get; set;}
        public DateTime EndDatePro {get; set;}
        public Guid TourCategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid EvaluationId { get; set; }
        public ICollection<TourProgramVM> TourPrograms { get; set; }
        public ICollection<PriceVM> Prices { get; set; }
    }
}