using ClubNet.WebSite.Common;
using ClubNet.WebSite.DataLayer.Configurations;
using ClubNet.WebSite.DataLayer.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ClubNet.WebSite.DataLayer.Extensions
{
    /// <summary>
    /// Data layer <see cref="IServiceCollection"/> extensions
    /// </summary>
    public static class IServiceCollectionExtension
    {
        #region Methods

        /// <summary>
        /// Add all the business layer services into the dependency injection system
        /// </summary>
        public static IServiceCollection AddDataLayerServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserStore<IUserInfo>, UserStoreService>();

            services.AddSingleton<IStorageServiceProvider, MongoDBStorageServiceProvider>();

            return services;
        }

        #endregion
    }
}
