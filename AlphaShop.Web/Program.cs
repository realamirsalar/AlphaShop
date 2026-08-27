using AlphaShop.Application;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Domain.Entities;
using AlphaShop.Infrastructure;
using AlphaShop.Infrastructure.Data;
using AlphaShop.Infrastructure.Identity;
using AlphaShop.Web.Mapping;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;              // Must have at least one number (0-9)
    options.Password.RequireLowercase = true;          // Must have at least one lowercase letter (a-z)
    options.Password.RequireUppercase = true;          // Must have at least one uppercase letter (A-Z)
    options.Password.RequireNonAlphanumeric = true;    // Must have at least one special character (!@#$%^&*)
    options.Password.RequiredLength = 7;               // Minimum 7 characters
    options.Password.RequiredUniqueChars = 1;          // Minimum unique characters

})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<PersianIdentityErrorDescriber>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/auth/login";
    options.LogoutPath = "/auth/Logout";
    options.AccessDeniedPath = "/auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});


builder.Services.AddInfrastructuerServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

var app = builder.Build();

await SeedDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();


async Task SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.InitializeAsync();
    }
}