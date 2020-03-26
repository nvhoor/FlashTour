using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Province : IEntityBase
    {
        //hoor
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public decimal Longitude { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public decimal Latitude { get; set; }
        public ICollection<Tour> Tours { get; set; }
    }
}
