using ClubNet.WebSite.DataLayer.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace ClubNet.WebSite.BusinessLayer.Extensions
{
    /// <summary>
    /// Busines layer <see cref="IServiceCollection"/> extensions
    /// </summary>
    public static class IServiceCollectionExtension
    {
        #region Methods

        /// <summary>
        /// Add all the business layer services into the dependency injection system
        /// </summary>
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddDataLayerServices();

            return services;
        }

        #endregion
    }
}
