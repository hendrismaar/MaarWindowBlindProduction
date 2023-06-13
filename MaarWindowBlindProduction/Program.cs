using MaarWindowBlindProduction.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(connectionString, sqlServerOptions =>
   {
       sqlServerOptions.EnableRetryOnFailure();
   }));


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Identity/Account/Login");
    options.LogoutPath = new PathString("/Identity/Account/Logout");
    options.AccessDeniedPath = new PathString("/Identity/Account/AccessDenied");
    options.Cookie.Path = "/";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.ReturnUrlParameter = "returnUrl"; // Add this line to include returnUrl parameter
});

// Add role services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Adds role policies

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllRolesPolicy", policy =>
    {
        policy.RequireRole("Admin", "Manufacturer", "Clothier","Packager", "Deliverer");
    });
    options.AddPolicy("ManufacturerPolicy", policy =>
    {
        policy.RequireRole("Admin", "Manufacturer");
    });
    options.AddPolicy("ClothierPolicy", policy =>
    {
        policy.RequireRole("Admin", "Clothier");
    });
    options.AddPolicy("PackagerPolicy", policy =>
    {
        policy.RequireRole("Admin", "Packager");
    });
    options.AddPolicy("DelivererPolicy", policy =>
    {
        policy.RequireRole("Admin", "Deliverer");
    });
    options.AddPolicy("AdminOnlyPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await SeedData.InitializeRolesAndUsers(serviceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
