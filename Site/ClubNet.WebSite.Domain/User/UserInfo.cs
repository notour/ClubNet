using System;

using ClubNet.WebSite.Common;

namespace ClubNet.WebSite.Domain.User
{
    /// <summary>
    /// Define all the user informations
    /// </summary>
    public class UserInfo : UserMinimalInfo, IUserInfo
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserInfo"/>
        /// </summary>
        public UserInfo(Guid id, string displayName, string login, string email, string passwordHash)
            : base(id, displayName)
        {
            Login = login;
            Email = email;
            PasswordHash = passwordHash;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the user login
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// Get the user email informations
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the user password hash
        /// </summary>
        public string PasswordHash { get; }

        #endregion
    }
}
