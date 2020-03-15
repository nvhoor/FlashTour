using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class TourCategory:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public String Name { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public String Description { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public String Image { get; set; }
    }
}
