using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Infrastructure
{
    public interface IPriceRepository : IRepository<Price>,IEnumerable<Price>
    {
        
    }
}