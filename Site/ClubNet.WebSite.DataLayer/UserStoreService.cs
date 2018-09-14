using System.Threading;
using System.Threading.Tasks;

using ClubNet.WebSite.Common;
using Microsoft.AspNetCore.Identity;

namespace ClubNet.WebSite.DataLayer
{
    /// <summary>
    /// Managed the user storage and retreive
    /// </summary>
    public sealed class UserStoreService : IUserStore<IUserInfo>
    {
        public Task<IdentityResult> CreateAsync(IUserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(IUserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task<IUserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IUserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(IUserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(IUserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserNameAsync(IUserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(IUserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(IUserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IUserInfo user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
