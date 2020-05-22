using System.Collections;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
 using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public IEnumerator<Contact> GetEnumerator()
        {
            foreach (var contact in _appContext.Contacts)
            {
                yield return contact;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}