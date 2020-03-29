
namespace AspNetCoreSpa.Infrastructure
{
    public interface IUnitOfWork
    {
//        ICustomerRepository Customers { get; }
//        IProductRepository Products { get; }
//        IProductCategoryRepository ProductCategories { get; }
//        IOrdersRepository Orders { get; }
        IBannerRepository Banners { get; }
        IAccountRepository Accounts { get; }
        IBookingPriceRepository BookingPrices { get; }
        IContactRepository Contacts { get; }
        IEvaluationRepository Evaluations { get; }
        IPostCategoryRepository PostCategories { get; }
        IPostRepository Posts { get; }
        IPriceRepository Prices { get; }
        IProvinceRepository Provinces { get; }
        IRoleRepository Roles { get; }
        ITourBookingRepository TourBookings { get; }
        ITourCategoryRepository TourCategories { get; }
        ITourCustomerRepository TourCustomers { get; }
        ITouristTypeRepository TouristTypes { get; }
        ITourProgramRepository TourPrograms { get; }
        ITourRepository Tours { get; }
        int SaveChanges();
    }
}
