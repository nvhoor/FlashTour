using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Role 
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string RoleName { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}