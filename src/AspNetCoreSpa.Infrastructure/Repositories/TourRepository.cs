﻿using System;
 using System.Collections;
 using System.Collections.Generic;
 using AspNetCoreSpa.Core.Entities;
 using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSpa.Infrastructure
{
    public class TourRepository : Repository<Tour>, ITourRepository
    {
        public TourRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public IEnumerator<Tour> GetEnumerator()
        {
            foreach (var tour in _appContext.Tours)
            {
              yield return  tour;
            }
        }

        public int DecreaseSlotsLeft(Guid id,int slots)
        {
            var curSlot = _appContext.Tours.Find(id).Slot;
            if (curSlot - slots < 0)
            {
                return -1;
            }
            else
            {
                curSlot = curSlot - slots;
                _appContext.Tours.Find(id).Slot = curSlot;
                _appContext.SaveChanges();
                return curSlot;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}