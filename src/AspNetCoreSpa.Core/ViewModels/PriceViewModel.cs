namespace AspNetCoreSpa.Core.ViewModels
{
    public class PriceViewModel
    {
        public int Id {get; set;}
        public int TourId {get;set;}
        public string Name {get; set;}
        public decimal OriginalPrice {get;set;}
        public decimal PromotionPrice {get;set;}
        public string TouristTypeName{get;set;}
    }
}