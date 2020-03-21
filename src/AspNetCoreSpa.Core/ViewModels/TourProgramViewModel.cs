using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourProgramViewModel
    {
        public int Id { get; set ; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int OrderNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DestinationId { get; set; }
        public string TourName { get; set; }
    }
}