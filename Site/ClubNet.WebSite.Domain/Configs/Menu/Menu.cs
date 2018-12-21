namespace ClubNet.WebSite.Domain.Configs.Menu
{
    using ClubNet.WebSite.Domain.Security;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a specific menu
    /// </summary>
    /// <seealso cref="Domain.Config.Menu.MenuItem" />
    [DataContract]
    public sealed class Menu : MenuItem
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        public Menu(Guid id, string name, string description, IEnumerable<MenuItem> items, SecurityCriteria securityCriteria)
            : base(id,
                   new LocalizedString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName, name),
                   new LocalizedString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName, name),
                   ConfigType.Menu,
                   securityCriteria)
        {
            Items = items;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu items
        /// </summary>
        [DataMember]
        public IEnumerable<MenuItem> Items { get; }

        #endregion
    }
}
