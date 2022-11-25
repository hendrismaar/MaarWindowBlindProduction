using MaarWindowBlindProduction.Models;
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
        public DbSet<PatternName> Patterns { get; set; }
        public DbSet<MaarWindowBlindProduction.Models.WindowBlind> WindowBlind { get; set; }

    }
}