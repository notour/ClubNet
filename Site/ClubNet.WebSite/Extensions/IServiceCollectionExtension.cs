namespace Microsoft.Extensions.DependencyInjection
{
    using ClubNet.Shared.Api.Contracts;
    using ClubNet.WebSite.Api;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.NusinessLayer.Extensions;
    using ClubNet.WebSite.Services;
    using ClubNet.WebSite.Tools;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.DataAnnotations;

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
            services.AddSingleton<IValidationAttributeAdapterProvider, LocalizedValidationAttributeAdapterProvider>();
            services.AddSingleton<IResourceService, ResourceServicesImpl>();

            services.AddScoped<IRequestService>((s) =>
            {
                var ctxAccessor = s.GetRequiredService<IHttpContextAccessor>();
                return ctxAccessor.CurrentRequestService();
            });

            services.AddSingleton<IFileService, FileServiceImpl>();
            services.AddSingleton<IApiService, ApiService>();

            // internal api access
            services.AddSingleton<IUserApi, UserController>();

            return services;
        }

        #endregion
    }
}
