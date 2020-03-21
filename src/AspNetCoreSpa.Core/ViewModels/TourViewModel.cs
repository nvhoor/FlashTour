namespace AspNetCoreSpa.Core.ViewModels
{
    public class TourViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Images { get; set; }
        public string Description { get; set; }
        public int DepartureId { get; set; }
        public int Slot { get; set; }
        public bool Censorship { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public string TourCategoryName { get; set; }
    }
}