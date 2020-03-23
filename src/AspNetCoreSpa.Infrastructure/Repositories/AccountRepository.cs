using AspNetCoreSpa.Core.Entities;
 using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}