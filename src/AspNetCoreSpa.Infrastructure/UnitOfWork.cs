namespace AspNetCoreSpa.Infrastructure
{

    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

//        ICustomerRepository _customers;
//        IProductCategoryRepository _productsCategories;
//        IProductRepository _products;
//        IOrdersRepository _orders;
         IBannerRepository _banners;
         IAccountRepository _accounts;
         IBookingPriceRepository _bookingPrices;
         IContactRepository _contacts ;
         IEvaluationRepository _evaluations ;
         IPostCategoryRepository _postCategories ;
         IPostRepository _posts ;
         IPriceRepository _prices ;
         IProvinceRepository _provinces ;
         IRoleRepository _roles ;
         ITourBookingDetailRepository _tourBookingDetails ;
         ITourBookingRepository _tourBookings ;
         ITourCategoryRepository _tourCategories ;
         ITourCustomerRepository _tourCustomers ;
         ITouristTypeRepository _touristTypes ;
         ITourProgramRepository _tourPrograms ;
         ITourRepository _tours ;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }



//        public ICustomerRepository Customers
//        {
//            get
//            {
//                if (_customers == null)
//                    _customers = new CustomerRepository(_context);
//
//                return _customers;
//            }
//        }
//
//
//
//        public IProductRepository Products
//        {
//            get
//            {
//                if (_products == null)
//                    _products = new ProductRepository(_context);
//
//                return _products;
//            }
//        }
//
//        public IProductCategoryRepository ProductCategories
//        {
//            get
//            {
//                if (_productsCategories == null)
//                    _productsCategories = new ProductCategoryRepository(_context);
//
//                return _productsCategories;
//            }
//        }
//
//
//
//        public IOrdersRepository Orders
//        {
//            get
//            {
//                if (_orders == null)
//                    _orders = new OrdersRepository(_context);
//
//                return _orders;
//            }
//        }


        public IBannerRepository Banners => _banners ?? (_banners = new BannerRepository(_context));

        public IAccountRepository Accounts => _accounts ?? (_accounts = new AccountRepository(_context));

        public IBookingPriceRepository BookingPrices => _bookingPrices ?? (_bookingPrices = new BookingPriceRepository(_context));

        public IContactRepository Contacts => _contacts ?? (_contacts = new ContactRepository(_context));

        public IEvaluationRepository Evaluations => _evaluations ?? (_evaluations = new EvaluationRepository(_context));

        public IPostCategoryRepository PostCategories => _postCategories ?? (_postCategories = new PostCategoryRepository(_context));

        public IPostRepository Posts => _posts ?? (_posts = new PostRepository(_context));

        public IPriceRepository Prices => _prices ?? (_prices = new PriceRepository(_context));

        public IProvinceRepository Provinces => _provinces ?? (_provinces = new ProvinceRepository(_context));

        public IRoleRepository Roles => _roles ?? (_roles = new RoleRepository(_context));

        public ITourBookingDetailRepository TourBookingDetails => _tourBookingDetails ?? (_tourBookingDetails = new TourBookingDetailRepository(_context));

        public ITourBookingRepository TourBookings => _tourBookings ?? (_tourBookings = new TourBookingRepository(_context));

        public ITourCategoryRepository TourCategories => _tourCategories ?? (_tourCategories = new TourCategoryRepository(_context));

        public ITourCustomerRepository TourCustomers => _tourCustomers ?? (_tourCustomers = new TourCustomerRepository(_context));

        public ITouristTypeRepository TouristTypes => _touristTypes ?? (_touristTypes = new TouristTypeRepository(_context));

        public ITourProgramRepository TourPrograms => _tourPrograms ?? (_tourPrograms = new TourProgramRepository(_context));

        public ITourRepository Tours => _tours ?? (_tours = new TourRepository(_context));

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
