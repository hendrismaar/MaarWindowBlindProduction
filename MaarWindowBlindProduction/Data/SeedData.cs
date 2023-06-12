using Microsoft.EntityFrameworkCore;
using MaarWindowBlindProduction.Models;
using Microsoft.AspNetCore.Identity;

namespace MaarWindowBlindProduction.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Pattern.Any())
                {
                    return;
                }
                context.Pattern.AddRange(
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

        public static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Check if the roles already exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Manufacturer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Manufacturer"));
            }

            if (!await roleManager.RoleExistsAsync("Clothier"))
            {
                await roleManager.CreateAsync(new IdentityRole("Clothier"));
            }

            if (!await roleManager.RoleExistsAsync("Packager"))
            {
                await roleManager.CreateAsync(new IdentityRole("Packager"));
            }

            if (!await roleManager.RoleExistsAsync("Deliverer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Deliverer"));
            }
        }
    }
}
