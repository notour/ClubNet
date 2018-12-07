using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ClubNet.WebSite.Areas.Identity.IdentityHostingStartup))]
namespace ClubNet.WebSite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //services.AddDbContext<ClubNetWebSiteContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("ClubNetWebSiteContextConnection")));

                //services.AddDefaultIdentity<ClubNetWebSiteUser>()
                //    .AddEntityFrameworkStores<ClubNetWebSiteContext>();
            });
        }
    }
}