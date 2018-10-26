using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClubNet.Framework.Memory;
using ClubNet.WebSite.Common;
using ClubNet.WebSite.Common.Contracts;
using ClubNet.WebSite.Common.Errors;
using ClubNet.WebSite.Domain.User;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.DependencyInjection;

namespace ClubNet.WebSite.DataLayer
{
    /// <summary>
    /// Managed the user storage and retreive
    /// </summary>
    public sealed class UserStoreService : Disposable, IUserStore<UserInfo>, IUserEmailStore<UserInfo>, IUserPasswordStore<UserInfo>
    {
        #region Fields

        private readonly IStorageService<UserInfo> _userStorage;
        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserStoreService"/>
        /// </summary>
        public UserStoreService(IStorageServiceProvider storageServiceProvider, IServiceProvider serviceProvider)
        {
            
            _serviceProvider = serviceProvider;
            _userStorage = storageServiceProvider.GetStorageService<UserInfo>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new user in the ClubNet system
        /// </summary>
        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var errorService = new Lazy<IErrorService>(() => this._serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IErrorService>());
            IdentityResult identity = null;
            try
            {
                user.Id = Guid.NewGuid();
                var userSaved = await _userStorage.CreateAsync(user, u => u.NormalizedEmail == user.NormalizedEmail, cancellationToken);

                if (userSaved != null && user.Id == userSaved.Id)
                    identity = IdentityResult.Success;
                else
                    identity = IdentityResult.Failed(new IdentityError()
                    {
                        Code = InternalErrorCodes.Conflict.GetCodeString(),
                        Description = errorService.Value.GetErrorDescription(ErrorCategory.User, InternalErrorCodes.Conflict, nameof(CreateAsync), user.NormalizedEmail)
                    });
            }
            catch (Exception ex)
            {
                var errorLoggedId = errorService.Value.LogError(ErrorCategory.User, InternalErrorCodes.InternalError, nameof(CreateAsync), ex, user.ToDiagnosticJson());
                identity = IdentityResult.Failed(new IdentityError()
                {
                    Code = InternalErrorCodes.InternalError.GetCodeString(),
                    Description = errorService.Value.GetErrorDescription(ErrorCategory.User, InternalErrorCodes.InternalError, errorLoggedId)
                });
            }

            return identity;
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserInfo> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _userStorage.FindFirstAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
        }

        public Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name.
        /// </summary>
        public async Task<UserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _userStorage.FindFirstAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        }

        /// <summary>
        /// Extract the email information from the <see cref="UserInfo"/>
        /// </summary>
        public Task<string> GetEmailAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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

        /// <summary>
        /// Set the current email addrress
        /// </summary>
        public Task SetEmailAsync(UserInfo user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(UserInfo user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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
            user.PasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(passwordHash));
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

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
