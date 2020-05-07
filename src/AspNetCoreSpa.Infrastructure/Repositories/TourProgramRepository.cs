using System.Collections;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class TourProgramRepository : Repository<TourProgram>, ITourProgramRepository, IEnumerable
    {
        public TourProgramRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext) _context;
        public IEnumerator<TourProgram> GetEnumerator()
        {
            foreach (var toup in _appContext.TourPrograms)
            {
                yield return toup;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}