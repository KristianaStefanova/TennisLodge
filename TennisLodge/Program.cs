using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Data.Seeding;
using TennisLodge.Data.Seeding.Interfaces;
using TennisLodge.Services.Core;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.Infrastructure.Extensions;
using TennisLodge.Web.Infrastructure.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<TennisLodgeDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    ConfigureIdentity(builder.Configuration, options);
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<TennisLodgeDbContext>();

builder.Services.AddRepositories(typeof(ITournamentRepository).Assembly);
builder.Services.AddUserDefineServices(typeof(ITournamentService).Assembly);
builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

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

app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.SeedDefaultIdentity();

app.UseAuthentication();
app.UseAuthorization();
app.UseAdminAccessRestriction();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

static void ConfigureIdentity(IConfigurationManager configurationManager,
    IdentityOptions identityOptions)
{
    identityOptions.SignIn.RequireConfirmedEmail = 
        configurationManager.GetValue<bool>($"IdentityConfig:SingIn:RequireConfirmedEmail");
    identityOptions.SignIn.RequireConfirmedAccount = 
        configurationManager.GetValue<bool>($"IdentityConfig:SingIn:RequireConfirmedAccount");
    identityOptions.SignIn.RequireConfirmedPhoneNumber =
        configurationManager.GetValue<bool>($"IdentityConfig:SingIn:RequireConfirmedPhoneNumber");
    

    identityOptions.Password.RequiredLength =
        configurationManager.GetValue<int>($"IdentityConfig:Password:RequiredLength");
    identityOptions.Password.RequireNonAlphanumeric =
        configurationManager.GetValue<bool>($"IdentityConfig:Password:RequireNonAlphanumeric");
    identityOptions.Password.RequiredUniqueChars = 
        configurationManager.GetValue<int>($"IdentityConfig:Password:RequiredUniqueChars");
}