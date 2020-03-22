using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class TourCategory:IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public string Description { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string Image { get; set; }
        public ICollection<Tour> Tours { get; set; }
    }
}
