namespace ClubNet.WebSite.BusinessLayer.Extensions
{
    using ClubNet.WebSite.BusinessLayer.Configurations;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.BusinessLayer.Logic;
    using ClubNet.WebSite.BusinessLayer.Services;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.DataLayer.Extensions;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Busines layer <see cref="IServiceCollection"/> extensions
    /// </summary>
    public static class IServiceCollectionExtension
    {
        #region Methods

        /// <summary>
        /// Add all the business layer services into the dependency injection system
        /// </summary>
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayerServices(configuration);

            services.Configure<DefaultConfiguration>(configuration.GetSection(DefaultConfiguration.ConfigurationSectionKey));

            services.AddSingleton<IConfigService, ConfigService>();

            services.AddSingleton<ISecurityBL, SecurityBL>();
            services.AddSingleton<IMenuBL, MenuBL>();
            services.AddSingleton<IThemeBL, ThemeBL>();

            services.AddSingleton<IUserBL, UserBL>();

            return services;
        }

        #endregion
    }
}
