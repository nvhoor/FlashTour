using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Tour : AuditableEntity, IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public String Name { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public String Image { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public String Images { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public String Description { get; set; }
        public DateTime DepartureDate { get; set; }
        public int DepartureId { get; set; }
        public int Slot { get; set; }
        public int Sensorship { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public int CategoryId { get; set; }
    }
}
