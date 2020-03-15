using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class ContactUs : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        [Column(TypeName = "NVARCHAR")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Email { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 5)]
        [Column(TypeName = "NVARCHAR")]
        public string Message { get; set; }

    }

}
