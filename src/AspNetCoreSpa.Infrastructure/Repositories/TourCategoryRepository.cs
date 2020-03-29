﻿using System.Collections;
 using System.Collections.Generic;
 using AspNetCoreSpa.Core.Entities;
using Microsoft.EntityFrameworkCore;
 namespace AspNetCoreSpa.Infrastructure
{
    public class TourCategoryRepository : Repository<TourCategory>, ITourCategoryRepository
    {
        public TourCategoryRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public IEnumerator<TourCategory> GetEnumerator()
        {
            foreach (var tourCate in _appContext.TourCategories)
            {
                yield return  tourCate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}