using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreSpa.Core.Entities
{
    public class PostCategory : IEntityBase
    {
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
