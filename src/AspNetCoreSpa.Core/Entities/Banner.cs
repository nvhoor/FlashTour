using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Banner : IEntityBase
    {
       [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string Image { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public string Description { get; set; }
    }
}