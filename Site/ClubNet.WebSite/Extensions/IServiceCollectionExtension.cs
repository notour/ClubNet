namespace Microsoft.Extensions.DependencyInjection
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.NusinessLayer.Extensions;
    using ClubNet.WebSite.Services;

    using Microsoft.AspNetCore.Http;

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

            services.AddScoped<IRequestService>((s) =>
            {
                var ctxAccessor = s.GetRequiredService<IHttpContextAccessor>();
                return ctxAccessor.CurrentRequestService();
            });

            services.AddSingleton<IFileService, FileServiceImpl>();

            return services;
        }

        #endregion
    }
}
