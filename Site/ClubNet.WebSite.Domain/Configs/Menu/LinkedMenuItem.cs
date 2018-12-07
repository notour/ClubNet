
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
        public LinkedMenuItem(Guid id, LocalizedString label, LocalizedString description, string url)
            : base(id, label, description, ConfigType.MenuLink)
        {
            Url = url;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the URL.
        /// </summary>
        public string Url { get; }

        #endregion

    }
}
