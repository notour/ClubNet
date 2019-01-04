﻿namespace ClubNet.WebSite.DataLayer
{
    using ClubNet.Framework.Memory;
    using ClubNet.WebSite.Common.Errors;
    using ClubNet.WebSite.Domain.User;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Managed the user storage and retreive
    /// </summary>
    public sealed class UserStoreService : Disposable, IUserStore<UserInfo>, IUserEmailStore<UserInfo>, IUserPasswordStore<UserInfo>, IUserLoginStore<UserInfo>
    {
        #region Fields

        private readonly IStorageService<UserInfo> _userStorage;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserStoreService"/>
        /// </summary>
        public UserStoreService(IStorageServiceProvider storageServiceProvider, ILogger<UserStoreService> logger)
        {
            this._userStorage = storageServiceProvider.GetStorageService<UserInfo>();
            this._logger = logger;
        }

        public Task AddLoginAsync(UserInfo user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new user in the ClubNet system
        /// </summary>
        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IdentityResult identity = null;
            try
            {
                user.Id = Guid.NewGuid();
                UserInfo userSaved = await this._userStorage.CreateAsync(user, u => u.NormalizedEmail == user.NormalizedEmail, cancellationToken);

                if (userSaved != null && user.Id == userSaved.Id)
                    identity = IdentityResult.Success;
                else
                    identity = IdentityResult.Failed(new IdentityError()
                    {
                        Code = ErrorCategory.User + "_" + InternalErrorCodes.Conflict.GetCodeString() + ":" + user.Email
                    });
            }
            catch (Exception ex)
            {
                var errorLoggedId = Guid.NewGuid();
                this._logger.LogError(ex, ErrorCategory.User + "_" + InternalErrorCodes.InternalError + "_" + nameof(CreateAsync) + ":" + user.ToDiagnosticJson());
                identity = IdentityResult.Failed(new IdentityError()
                {
                    Code = ErrorCategory.User + "_" + InternalErrorCodes.InternalError.GetCodeString() + ":" + errorLoggedId,
                });
            }

            return identity;
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Find the user by email
        /// </summary>
        public async Task<UserInfo> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await this._userStorage.FindFirstAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
        }

        /// <summary>
        /// Found the user informations by his unique id
        /// </summary>
        public async Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var userGuidId = Guid.Parse(userId);
            return await this._userStorage.FindFirstAsync(u => u.Id == userGuidId, cancellationToken);
        }

        /// <summary>
        /// Find the user by the login value
        /// </summary>
        public Task<UserInfo> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name.
        /// </summary>
        public async Task<UserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await this._userStorage.FindFirstAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        }

        /// <summary>
        /// Extract the email information from the <see cref="UserInfo"/>
        /// </summary>
        public Task<string> GetEmailAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Get a value indicating if the current user confirm his email address
        /// </summary>
        public Task<bool> GetEmailConfirmedAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the normalized email from the user
        /// </summary>
        public Task<string> GetNormalizedEmailAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the user password hash
        /// </summary>
        public Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        /// Gets the specific user id
        /// </summary>
        public Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            if (user.Id == default(Guid))
                return Task.FromResult(string.Empty);

            return Task.FromResult(user.Id.ToString());
        }

        /// <summary>
        /// Get the user name from the <see cref="UserInfo"/>
        /// </summary>
        public Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        /// <summary>
        /// Gets a value indicating if the current <see cref="UserInfo"/> have a password
        /// </summary>
        public Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task RemoveLoginAsync(UserInfo user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set the current email addrress
        /// </summary>
        public Task SetEmailAsync(UserInfo user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Set a value indicating that the email have been confirmed
        /// </summary>
        public Task SetEmailConfirmedAsync(UserInfo user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Set the normalized email
        /// </summary>
        public Task SetNormalizedEmailAsync(UserInfo user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Set the normalized user name
        /// </summary>
        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Store the current password hash
        /// </summary>
        public Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Assign the current user name into the <see cref="UserInfo"/> object
        /// </summary>
        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Update the current user info
        /// </summary>
        public async Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            UserInfo savedUserInfo = await this._userStorage.SaveAsync(user, cancellationToken);
            return IdentityResult.Success;
        }

        #endregion
    }
}
