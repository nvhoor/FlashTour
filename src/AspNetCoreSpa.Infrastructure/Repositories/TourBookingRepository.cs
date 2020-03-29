﻿using System.Collections;
 using System.Collections.Generic;
 using AspNetCoreSpa.Core.Entities;
 using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class TourBookingRepository : Repository<TourBooking>, ITourBookingRepository
    {
        public TourBookingRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public IEnumerator<TourBooking> GetEnumerator()
        {
            foreach (var tourBooking in _appContext.TourBookings)
            {
                yield return  tourBooking;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}