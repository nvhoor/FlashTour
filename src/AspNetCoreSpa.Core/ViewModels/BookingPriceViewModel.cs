namespace AspNetCoreSpa.Core.ViewModels
{
    public class BookingPriceViewModel
    {
        public int Id {get; set;}
        public int BookingDetailId {get; set;}
        public int TouristTypeId {get; set;}
        public decimal Price {get; set;}
    }
}