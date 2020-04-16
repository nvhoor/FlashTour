using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Infrastructure
{
    public interface ITourCustomerRepository : IRepository<TourCustomer>, IEnumerable<TourCustomer>
    {
        
    }
}