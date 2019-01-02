using System;
using System.Globalization;

namespace ClubNet.WebSite.Common
{
    /// <summary>
    /// Contains that expose only the basic user information needed
    /// </summary>
    public interface IUserInfo
    {
        #region Properties

        /// <summary>
        /// Gets the user unique id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the login user informartion
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the user email address
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets the simple display information we should use on display
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the preferred culture
        /// </summary>
        CultureInfo PreferredCulture { get; }

        #endregion
    }
}
