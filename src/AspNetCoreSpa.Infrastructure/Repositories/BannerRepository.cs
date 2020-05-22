using System.Collections;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        public BannerRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public IEnumerator<Banner> GetEnumerator()
        {
            foreach (var banner in _appContext.Banners)
            {
                yield return banner;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}