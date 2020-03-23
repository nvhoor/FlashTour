using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Contact : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email {get; set;}
        [Column(TypeName = "VARCHAR(20)")]
        public string Phone {get; set;}
        [Column(TypeName = "NVARCHAR(255)")]
        public string Address {get; set;}
        [Column(TypeName = "NVARCHAR(100)")]
        public string Title {get; set;}
    }
}