using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Account 
    {
        [Key]
        [Column(TypeName = "VARCHAR(100)")]
        public string Username { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }
        [Column(TypeName = "NVARCHAR(255)")]
        public string Password { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string Avatar { get; set; }
        [EmailAddress]
        public string Email {get; set;}
        [Column(TypeName = "VARCHAR(20)")]
        public string Phone {get; set;}
        public DateTime BirthDay {get; set;}
        [Column(TypeName = "NVARCHAR(255)")]
        public string Address {get; set;}
        public bool Deleted {get; set;}
        public int RoleID {get; set;}
    }
}