using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Infrastructure
{
    public interface IBannerRepository : IRepository<Banner>, IEnumerable<Banner>
    {
        
    }
}