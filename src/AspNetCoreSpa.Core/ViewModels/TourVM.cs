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
        public int Slot { get; set; }
        public bool Censorship { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public Guid TourCategoryId { get; set; }
        public string CategoryName { get; set; }
        public TourCategory TourCategory { get; set; }
        public ICollection<EvaluationVM> Evaluations { get; set; } 
    }
}