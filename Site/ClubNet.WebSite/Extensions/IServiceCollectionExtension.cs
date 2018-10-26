using ClubNet.WebSite.Common.Contracts;
using ClubNet.WebSite.Domain.User;
using ClubNet.WebSite.Services;
using ClubNet.WebSite.Services.Tools;

using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods on the <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtension
    {
        #region Methods

        /// <summary>
        /// Initialize all the services used all along the application
        /// </summary>
        public static IServiceCollection AddClubNetViewServices(this IServiceCollection services)
        {
            services.AddSingleton<IResourceService, ResourceServicesImpl>();

            return services;
        }

        #endregion
    }
}
