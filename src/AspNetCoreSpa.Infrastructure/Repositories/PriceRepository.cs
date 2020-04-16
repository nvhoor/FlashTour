using System;
using System.Collections;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
 using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        public PriceRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public IEnumerator<Price> GetEnumerator()
        {
            foreach (var price in _appContext.Prices)
            {
                yield return price;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}