namespace ClubNet.WebSite.Domain.Configs.Menu
{
    using System;

    /// <summary>
    /// Define a menu item
    /// </summary>
    /// <seealso cref="ClubNet.WebSite.Domain.Entity{ClubNet.WebSite.Domain.Configs.ConfigType}" />
    public class MenuItem : Entity<ConfigType>
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        protected MenuItem(Guid id, LocalizedString label, LocalizedString description, ConfigType entityType)
            : base(id, entityType)

        {
            Label = label;
            Description = description;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the label.
        /// </summary>
        public LocalizedString Label { get; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public LocalizedString Description { get; }

        #endregion
    }
}
