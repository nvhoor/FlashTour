﻿using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;

namespace AspNetCoreSpa.Infrastructure
{
    public interface ITourProgramRepository : IRepository<TourProgram>,IEnumerable<TourProgram>
    {
        
    }
}