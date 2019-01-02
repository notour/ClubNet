[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ClubNet.Website.Services.UTest")]

namespace Microsoft.Extensions.DependencyInjection
{
    using ClubNet.WebSite.Domain.Security;
    using ClubNet.WebSite.Domain.User;
    using ClubNet.WebSite.Services.Tools;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Extension methods on the <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtension
    {
        #region Methods

        /// <summary>
        /// Initialize all the services used all along the application
        /// </summary>
        public static IServiceCollection AddClubNetToolsServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher<UserInfo>, Sha1PasswodHasher>();

            return services;
        }

        /// <summary>
        /// Initialize all the services dedicated to the user managements
        /// </summary>
        public static IServiceCollection AddClubNetUserServices(this IServiceCollection service)
        {
            service.AddIdentity<UserInfo, UserRole>(cfg =>
            {
                // Password rules
                cfg.Password.RequireUppercase = true;
                cfg.Password.RequireLowercase = true;
                cfg.Password.RequireDigit = true;
                cfg.Password.RequiredLength = 6;

                // signin option
                cfg.SignIn.RequireConfirmedEmail = true;

                cfg.User.RequireUniqueEmail = true;
                cfg.Lockout.MaxFailedAccessAttempts = 5;

            })
            .AddUserManager<UserManager<UserInfo>>()
            .AddRoleManager<RoleManager<UserRole>>()
            .AddDefaultTokenProviders();

            return service;
        }

        #endregion
    }
}
