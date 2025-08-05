using Microsoft.EntityFrameworkCore;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.GCommon;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.Infrastructure.Extensions;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
              ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<TennisLodgeDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<TennisLodgeDbContext>();

            builder.Services.AddRepositories(typeof(ITournamentRepository).Assembly);
            builder.Services.AddUserDefineServices(typeof(ITournamentService).Assembly);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(ApplicationConstants.AllowAllDomainsPolicy, policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins("https://localhost:7019")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddAntiforgery(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(AllowAllDomainsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapIdentityApi<ApplicationUser>();
            app.MapControllers();

            app.Run();
        }
    }
}
