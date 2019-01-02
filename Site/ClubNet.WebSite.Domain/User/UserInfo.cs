using System;
using System.Globalization;
using ClubNet.WebSite.Common;

using Microsoft.AspNetCore.Identity;

namespace ClubNet.WebSite.Domain.User
{
    /// <summary>
    /// Define all the user informations
    /// </summary>
    public class UserInfo : IdentityUser<Guid>, IUserInfo
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserInfo"/>
        /// </summary>
        public UserInfo()
        {
        }

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserInfo"/>
        /// </summary>
        public UserInfo(string userName)
            : base(userName)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the simple display information we should use on display
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the user preferred culture
        /// </summary>
        public CultureInfo PreferredCulture { get; set; }

        #endregion
    }
}
