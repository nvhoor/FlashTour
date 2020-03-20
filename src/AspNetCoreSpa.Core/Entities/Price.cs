using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreSpa.Core.Entities
{
    public class Price : IEntityBase{
        [Key]
        public int Id {get; set;}
        public int TourId {get;set;}
        [Column(TypeName="NVARCHAR(100)")]
        public string Name {get; set;}
        [Column(TypeName="decimal(18,2)")]
        public decimal OriginalPrice {get;set;}
        [Column(TypeName="decimal(18,2)")]
        public decimal PromotionPrice {get;set;}
        public DateTime StartDatePro {get; set;}
        public int TouristTypeId {get;set;}
        public TouristType TouristType { get; set; }
    }
}