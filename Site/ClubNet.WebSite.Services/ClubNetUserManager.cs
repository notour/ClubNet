using System;
using System.Collections.Generic;

using ClubNet.WebSite.Common;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ClubNet.WebSite.Services
{
    /// <summary>
    /// Manager in charge of identifyng and store user infomations
    /// </summary>
    public sealed class ClubNetUserManager : UserManager<IUserInfo>, IUserManager
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ClubNetUserManager"/>
        /// </summary>
        public ClubNetUserManager(IUserStore<IUserInfo> store,
                                  IOptions<IdentityOptions> optionsAccessor,
                                  IPasswordHasher<IUserInfo> passwordHasher,
                                  IEnumerable<IUserValidator<IUserInfo>> userValidators,
                                  IEnumerable<IPasswordValidator<IUserInfo>> passwordValidators,
                                  ILookupNormalizer keyNormalizer,
                                  IdentityErrorDescriber errors,
                                  IServiceProvider services,
                                  ILogger<UserManager<IUserInfo>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        #endregion

        #region Methods

        #endregion

    }
}
