﻿namespace ClubNet.WebSite.DataLayer.Extensions
{
    using ClubNet.WebSite.DataLayer.Configurations;
    using ClubNet.WebSite.DataLayer.Services;
    using ClubNet.WebSite.Domain.Security;
    using ClubNet.WebSite.Domain.User;
    using ClubNet.WebSite.Tools;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Data layer <see cref="IServiceCollection"/> extensions
    /// </summary>
    public static class IServiceCollectionExtension
    {
        #region Methods

        /// <summary>
        /// Add all the business layer services into the dependency injection system
        /// </summary>
        public static IServiceCollection AddDataLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserClaimsPrincipalFactory<UserInfo>, UserInfoClaimsPrincipalFactory>();

            services.AddSingleton<IUserStore<UserInfo>, UserStoreService>();
            services.AddSingleton<IUserEmailStore<UserInfo>>(i => (IUserEmailStore<UserInfo>)i.GetService<IUserStore<UserInfo>>());
            services.AddSingleton<IUserPasswordStore<UserInfo>>(i => (IUserPasswordStore<UserInfo>)i.GetService<IUserStore<UserInfo>>());
            services.AddSingleton<IUserLoginStore<UserInfo>>(i => (IUserLoginStore<UserInfo>)i.GetService<IUserStore<UserInfo>>());

            services.AddSingleton<IRoleStore<UserRole>, UserRoleStoreService>();

            services.AddSingleton<IStorageServiceProvider, MongoDBStorageServiceProvider>();

            services.Configure<MongoDBConfiguration>(configuration.GetSection(MongoDBConfiguration.ConfigurationSectionKey));
            

            return services;
        }

        #endregion
    }
}
