using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
namespace AspNetCoreSpa.Infrastructure
{
    public interface ITourCategoryRepository : IRepository<TourCategory>,IEnumerable<TourCategory>
    {
        
    }
}