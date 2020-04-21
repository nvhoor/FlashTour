using System.Collections;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class TourCustomerRepository:Repository<TourCustomer>, ITourCustomerRepository
    {
        public TourCustomerRepository(DbContext context) : base(context)
        { }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public IEnumerator<TourCustomer> GetEnumerator()
        {
            foreach (var tourCus in _appContext.TourCustomers)
            {
                yield return  tourCus;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}