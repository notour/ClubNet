
using ClubNet.WebSite.Domain.Security;
using System;

namespace ClubNet.WebSite.Domain.Configs.Menu
{
    /// <summary>
    /// Define a menu item used to navigate to a specific link
    /// </summary>
    /// <seealso cref="ClubNet.WebSite.Domain.Configs.Menu.MenuItem" />
    public sealed class LinkedMenuItem : MenuItem
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedMenuItem"/> class.
        /// </summary>
        public LinkedMenuItem(Guid id, LocalizedString label, LocalizedString description, string controller, string action, string url, SecurityCriteria securityCriteria)
            : base(id, label, description, ConfigType.MenuLink, securityCriteria)
        {
            Controller = controller;
            Action = action;
            Url = url;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the controller name.
        /// </summary>
        public string Controller { get; }

        /// <summary>
        /// Gets the controller action
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Gets the direct URL
        /// </summary>
        public string Url { get; }
        #endregion

    }
}
