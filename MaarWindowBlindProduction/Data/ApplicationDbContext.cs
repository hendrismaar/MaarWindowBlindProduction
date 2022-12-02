﻿using MaarWindowBlindProduction.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaarWindowBlindProduction.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PatternName> Pattern { get; set; }
        public DbSet<WindowBlind> WindowBlind { get; set; }

    }
}