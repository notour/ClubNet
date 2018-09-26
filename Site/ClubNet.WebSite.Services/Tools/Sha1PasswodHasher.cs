using System.IO;
using System.Security.Cryptography;
using System.Text;
using ClubNet.WebSite.Domain.User;

using Microsoft.AspNetCore.Identity;

namespace ClubNet.WebSite.Services.Tools
{
    /// <summary>
    /// Hasher in charge of encrypt and compare passwords
    /// </summary>
    public sealed class Sha1PasswodHasher : IPasswordHasher<UserInfo>
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="Sha1PasswodHasher"/>
        /// </summary>
        public Sha1PasswodHasher()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use a sha1 algorithme to generate a password hash
        /// </summary>
        public string HashPassword(UserInfo user, string password)
        {
            using (var hasher = SHA1Managed.Create())
            {
                using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(password)))
                {
                    var resultHash = hasher.ComputeHash(memoryStream);
                    return Encoding.UTF8.GetString(resultHash);
                }
            }
        }

        public PasswordVerificationResult VerifyHashedPassword(UserInfo user, string hashedPassword, string providedPassword)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
