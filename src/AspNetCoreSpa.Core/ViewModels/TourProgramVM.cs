using System;
using System.ComponentModel.DataAnnotations;
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
        public string Destination { get; set; }
        public Guid TourId { get; set; }
    }
}