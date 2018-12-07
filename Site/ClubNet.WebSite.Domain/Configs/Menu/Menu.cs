namespace ClubNet.WebSite.Domain.Configs.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Define a specific menu
    /// </summary>
    /// <seealso cref="ClubNet.WebSite.Domain.Config.Menu.MenuItem" />
    public sealed class Menu : MenuItem
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        public Menu(Guid id, string name, string description, IEnumerable<MenuItem> items)
            : base(id,
                   new LocalizedString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName, name),
                   new LocalizedString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName, name),
                   Configs.ConfigType.Menu)
        {
        }

        protected Menu(Guid id, LocalizedString label, LocalizedString description)
            : base(id, label, description, ConfigType.Menu)

        {
        }

        #endregion
    }
}
