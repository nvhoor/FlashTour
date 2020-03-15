using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Provinces : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
