using ClubNet.WebSite.BusinessLayer.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ClubNet.Website.ClubNet.WebSite.UTest")]

namespace ClubNet.WebSite
{
    /// <summary>
    /// Define all the start and configuration process
    /// </summary>
    public class Startup
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="Startup"/>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the configuration tools
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Configuration and service initialization
        /// </summary>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddClubNetViewServices();

            services.AddBusinessLayerServices(Configuration);

            services.AddClubNetToolsServices()
                    .AddClubNetUserServices();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        /// <summary>
        /// Configure the http pipeline
        /// </summary
        /// <remarks>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </remarks>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{lang=en-us}/{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion
    }
}
