using System;
using ClubNet.WebSite.Areas.Identity.Data;
using ClubNet.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ClubNet.WebSite.Areas.Identity.IdentityHostingStartup))]
namespace ClubNet.WebSite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ClubNetWebSiteContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ClubNetWebSiteContextConnection")));

                services.AddDefaultIdentity<ClubNetWebSiteUser>()
                    .AddEntityFrameworkStores<ClubNetWebSiteContext>();
            });
        }
    }
}