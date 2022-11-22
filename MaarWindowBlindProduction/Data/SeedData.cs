using Microsoft.EntityFrameworkCore;
using MaarWindowBlindProduction.Models;

namespace MaarWindowBlindProduction.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Patterns.Any())
                {
                    return;
                }
                context.Patterns.AddRange(
                    new PatternName { Pattern = "Polka dots" },
                    new PatternName { Pattern = "Moroccan" },
                    new PatternName { Pattern = "Quatrefoil" },
                    new PatternName { Pattern = "Chevron" },
                    new PatternName { Pattern = "Honeycomb" },
                    new PatternName { Pattern = "Houndstooth" },
                    new PatternName { Pattern = "Ikat" },
                    new PatternName { Pattern = "Fret / Greek key" },
                    new PatternName { Pattern = "Damask" },
                    new PatternName { Pattern = "Herringbone" },
                    new PatternName { Pattern = "Argyle" },
                    new PatternName { Pattern = "Ogee" },
                    new PatternName { Pattern = "Paisley / Botha" },
                    new PatternName { Pattern = "Gingham / Vichy" },
                    new PatternName { Pattern = "Floral" },
                    new PatternName { Pattern = "Scallops / Scale" },
                    new PatternName { Pattern = "Lattice" },
                    new PatternName { Pattern = "Stripes" },
                    new PatternName { Pattern = "Fleur de lis" },
                    new PatternName { Pattern = "Basketweave" },
                    new PatternName { Pattern = "Cube" },
                    new PatternName { Pattern = "Harlequin" },
                    new PatternName { Pattern = "Plaid" },
                    new PatternName { Pattern = "Grunge" });
                context.SaveChanges();
            }
        }
    }
}
