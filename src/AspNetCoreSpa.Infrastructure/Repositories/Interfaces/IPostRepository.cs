using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
namespace AspNetCoreSpa.Infrastructure
{
    public interface IPostRepository : IRepository<Post>, IEnumerable<Post>
    {
        
    }
}