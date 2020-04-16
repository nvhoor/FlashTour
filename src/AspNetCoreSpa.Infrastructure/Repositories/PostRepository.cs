using System.Collections;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        { }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public IEnumerator<Post> GetEnumerator()
        {
            foreach (var post in _appContext.Posts)
            {
                yield return post;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}