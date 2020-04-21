using System.Collections;
using System.Collections.Generic;
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
        public IEnumerator<PostCategory> GetEnumerator()
        {
            foreach (var postCate in _appContext.PostCategories)
            {
                yield return postCate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}