using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Culture
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(255)")]
        public string Name { get; set; }
        public virtual List<Resource> Resources { get; set; }
    }
}
