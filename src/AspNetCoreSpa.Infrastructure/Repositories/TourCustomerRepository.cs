using AspNetCoreSpa.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class TourCustomerRepository:Repository<TourCustomer>, ITourCustomerRepository
    {
        public TourCustomerRepository(DbContext context) : base(context)
        { }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}