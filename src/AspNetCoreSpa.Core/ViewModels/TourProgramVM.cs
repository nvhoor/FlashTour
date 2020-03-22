using System;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourProgramVM
    {
        public Guid Id { get; set ; }
        public DateTime Date { get; set; }
        public int OrderNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DestinationId { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }  
    }
}