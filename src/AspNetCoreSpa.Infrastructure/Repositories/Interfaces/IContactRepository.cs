using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Infrastructure
{
    public interface IContactRepository : IRepository<Contact>, IEnumerable<Contact>
    {
    }
}