using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Tour : AuditableEntity, IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string Image { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string Images { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public string Description { get; set; }
        public DateTime DepartureDate { get; set; }
        public int DepartureId { get; set; }
        public int Slot { get; set; }
        public int Censorship { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public int CategoryId { get; set; }
        public TourCategory TourCategory { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
