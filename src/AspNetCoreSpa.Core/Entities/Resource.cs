using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string Key { get; set; }
        [Column(TypeName = "NVARCHAR(500)")]
        public string Value { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
