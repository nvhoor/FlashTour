using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class TouristType : IEntityBase
    {
        [Key]
        public Guid Id {get; set;}
        [Column(TypeName="NVARCHAR(100)")]
        public string Name {get; set;}
        public ICollection<TourCustomer> TourCustomer { get; set; }
        public ICollection<Price> Price { get; set; }
    }
}