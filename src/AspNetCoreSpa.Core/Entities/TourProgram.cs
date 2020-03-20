using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class TourProgram : IEntityBase
    {
        [Key]
        public int Id { get; set ; }
        public DateTime Date { get; set; }
        public int OrderNumber { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Title { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public string Description { get; set; }
        public int DestinationId { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        
    }
}
