using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCoreSpa.Core.Entities
{
    public class Post : AuditableEntity, IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public String Name { get; set; }
        [Column(TypeName = "NVARCHAR(200)")]
        public String PostContent { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public String Description { get; set; }
        [Column(TypeName = "NVARCHAR(255)")]
        public String Image { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public String MetaDescription { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public String MetaKeyWord { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public String Alias { get; set; }
        public bool Status { get; set; }
        public bool Censorship { get; set; }
        public bool Deleted { get; set; }
        public int PostCategoryId { get; set; }
        public PostCategory PostCategory { get; set; }
    }
}
