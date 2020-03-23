using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Province : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public ICollection<Tour> Tours { get; set; }
        public ICollection<TourProgram> TourPrograms { get; set; }
    }
}
