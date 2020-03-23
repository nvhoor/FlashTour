using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSpa.Core.Entities
{
    public class Evaluation : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public int OneStar { get; set; }
        public int TwoStar { get; set; }
        public int ThreeStar { get; set; }
        public int FourStar { get; set; }
        public int FiveStar { get; set; }
        public Guid TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
