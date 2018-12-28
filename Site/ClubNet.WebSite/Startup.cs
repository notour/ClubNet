[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ClubNet.Website.ClubNet.WebSite.UTest")]

namespace ClubNet.WebSite
{
    using ClubNet.WebSite.BusinessLayer.Extensions;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Services;
    using ClubNet.WebSite.Tools;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Localization;

    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Define all the start and configuration process
    /// </summary>
    public class Startup
    {
        #region Fields

        private readonly IClubDescriptor _clubDescriptor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="Startup"/>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var clubSelection = Configuration.GetValue<string>("Club");

            if (!string.IsNullOrEmpty(clubSelection))
            {
                var assembly = Assembly.LoadFile(Path.Combine(Path.GetDirectoryName(typeof(Startup).Assembly.Location),
                                                              "Clubs",
                                                              clubSelection, nameof(ClubNet) + "." + clubSelection + ".dll"));

                var descriptors = (from type in assembly.GetTypes()
                                   where typeof(IClubDescriptor).IsAssignableFrom(type)
                                   select type).ToArray();

                if (!descriptors.Any() || descriptors.Length > 1)
                    throw new InvalidDataException("None or multiple " + nameof(IClubDescriptor));

                _clubDescriptor = (IClubDescriptor)Activator.CreateInstance(descriptors.Single());
            }
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
            if (_clubDescriptor != null)
                services.AddSingleton(_clubDescriptor);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var clubAssembly = _clubDescriptor.GetType().GetTypeInfo().Assembly;

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new TenantViewLocationExpander());

                if (_clubDescriptor != null) // important to keep string.Empty as second parameters to allow path solving
                    options.FileProviders.Add(new EmbeddedFileProvider(clubAssembly, string.Empty));
            });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("lang", typeof(LanguageRouteConstraint));
            });

            services.AddClubNetViewServices();

            services.AddBusinessLayerServices(Configuration);

            services.AddClubNetToolsServices()
                    .AddClubNetUserServices();

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Account/Login";
                options.LogoutPath = $"/Account/Logout";
                options.AccessDeniedPath = $"/Account/AccessDenied";
            });

            services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactoryImpl>();
            services.AddHttpContextAccessor();
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
                app.UseExceptionHandler("en/Home/Error");
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
                    template: "{lang}/{controller}/{action}/{id?}",
                    defaults: new { lang = "en", controller = "Home", action = "Index" });
            });

            if (_clubDescriptor != null)
                this._clubDescriptor.Configure(app);
        }

        #endregion
    }
}
