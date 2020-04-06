using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Infrastructure
{
    public interface ITourRepository : IRepository<Tour>,IEnumerable<Tour>
    {
        int DecreaseSlotsLeft(Guid id,int slots);
    }
}