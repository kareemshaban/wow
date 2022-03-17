using System;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Fitness.Areas.Identity.IdentityHostingStartup))]
namespace Fitness.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<FitnessDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("FitnessDbContextConnection")));


                services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddDefaultUI()
             .AddRoles<IdentityRole>()
             .AddRoleManager<RoleManager<IdentityRole>>()
             .AddDefaultTokenProviders()
             .AddEntityFrameworkStores<FitnessDbContext>();
            });
        }
    }
}