namespace Microsoft.Extensions.DependencyInjection
{
    using ClubNet.Shared.Api.Contracts;
    using ClubNet.WebSite;
    using ClubNet.WebSite.Api;
    using ClubNet.WebSite.BusinessLayer.Services;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Resources.Emails;
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
            services.AddSingleton<IEmailTemplateProvider>(o =>
            {
                return new EmailTemplateProvider(typeof(Startup).Assembly, o.GetService<IClubDescriptor>(), EmailSubjects.ResourceManager);
            });

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
