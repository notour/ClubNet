using ClubNet.WebSite.Common;

using Microsoft.AspNetCore.Identity;

namespace ClubNet.WebSite.Services.Tools
{
    /// <summary>
    /// Hasher in charge of encrypt and compare passwords
    /// </summary>
    public sealed class Sha1PasswodHasher : IPasswordHasher<IUserInfo>
    {
        #region Methods

        public string HashPassword(IUserInfo user, string password)
        {
            throw new System.NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(IUserInfo user, string hashedPassword, string providedPassword)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
