using AspNetCoreSpa.Core.Entities;
 using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class PostCategoryRepository : Repository<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(DbContext context) : base(context)
        {
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext) _context;
    }
}