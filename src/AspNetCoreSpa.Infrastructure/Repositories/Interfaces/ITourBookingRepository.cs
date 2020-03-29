using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Infrastructure
{
    public interface ITourBookingRepository : IRepository<TourBooking>,IEnumerable<TourBooking>
    {
        
    }
}