﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using GrubTime.Models;
using Microsoft.EntityFrameworkCore;

namespace GrubTime.Models
{
    public class GrubTimeContext: DbContext
    {
        public GrubTimeContext(DbContextOptions<GrubTimeContext> options)
            : base(options) { }

        public DbSet<StarredPlaces> StarredPlaces { get; set; }
    }
}
