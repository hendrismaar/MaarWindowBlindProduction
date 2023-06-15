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

            try
            {
                // Create users
                var admin = new IdentityUser {  UserName = "admin@blinds.com", Email = "admin@blinds.com", EmailConfirmed = true };
                var manufacturer = new IdentityUser { UserName = "manufacturer@blinds.com", Email = "manufacturer@blinds.com", EmailConfirmed = true };
                var clothier = new IdentityUser { UserName = "clothier@blinds.com", Email = "clothier@blinds.com", EmailConfirmed = true };
                var packager = new IdentityUser { UserName = "packager@blinds.com", Email = "packager@blinds.com", EmailConfirmed = true };
                var deliverer = new IdentityUser { UserName = "deliverer@blinds.com", Email = "deliverer@blinds.com", EmailConfirmed = true };

                await userManager.CreateAsync(admin, "Password123!");
                await userManager.CreateAsync(manufacturer, "Password123!");
                await userManager.CreateAsync(clothier, "Password123!");
                await userManager.CreateAsync(packager, "Password123!");
                await userManager.CreateAsync(deliverer, "Password123!");
                
                // Assign roles to users
                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(manufacturer, "Manufacturer");
                await userManager.AddToRoleAsync(clothier, "Clothier");
                await userManager.AddToRoleAsync(packager, "Packager");
                await userManager.AddToRoleAsync(deliverer, "Deliverer");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
