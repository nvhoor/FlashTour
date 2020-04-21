using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Infrastructure
{
    public interface IPostCategoryRepository : IRepository<PostCategory>,IEnumerable<PostCategory>
    {
        
    }
}