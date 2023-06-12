using Microsoft.EntityFrameworkCore;
using MaarWindowBlindProduction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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

        public static async Task InitializeRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

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

            // Create users
            var admin = new IdentityUser { Email = "admin@blinds.com" };
            var manufacturer = new IdentityUser { Email = "manufacturer@blinds.com" };
            var clothier = new IdentityUser { Email = "clothier@blinds.com" };
            var packager = new IdentityUser { Email = "packager@blinds.com" };
            var deliverer = new IdentityUser { Email = "deliverer@blinds.com" };

            await userManager.CreateAsync(admin);
            await userManager.CreateAsync(manufacturer);
            await userManager.CreateAsync(clothier);
            await userManager.CreateAsync(packager);
            await userManager.CreateAsync(deliverer);

            // Assign roles to users
            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.AddToRoleAsync(manufacturer, "Manufacturer");
            await userManager.AddToRoleAsync(clothier, "Clothier");
            await userManager.AddToRoleAsync(packager, "Packager");
            await userManager.AddToRoleAsync(deliverer, "Deliverer");
        }
    }
}
