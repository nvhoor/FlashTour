using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Infrastructure.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace AspNetCoreSpa.Infrastructure
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddInfrastructurServices(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizerFactory, EFStringLocalizerFactory>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<IApplicationDataService, ApplicationDataService>();
            services.AddScoped<IUnitOfWork, HttpUnitOfWork>();
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<ISeedData, SeedData>();
//            services.AddTransient<ICustomerRepository, CustomerRepository>();
//            services.AddTransient<IOrdersRepository, OrdersRepository>();
//            services.AddTransient<IProductRepository, ProductRepository>();
//            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IBannerRepository, BannerRepository>();
            services.AddTransient<IBookingPriceRepository, BookingPriceRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IEvaluationRepository, EvaluationRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostCategoryRepository, PostCategoryRepository>();
            services.AddTransient<IPriceRepository, PriceRepository>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ITourRepository, TourRepository>();
            services.AddTransient<ITourBookingRepository, TourBookingRepository>();
            services.AddTransient<ITourBookingDetailRepository, TourBookingDetailRepository>();
            services.AddTransient<ITourCategoryRepository, TourCategoryRepository>();
            services.AddTransient<ITourCustomerRepository, TourCustomerRepository>();
            services.AddTransient<ITouristTypeRepository, TouristTypeRepository>();
            services.AddTransient<ITourProgramRepository, TourProgramRepository>();
            return services;
        }
    }
}