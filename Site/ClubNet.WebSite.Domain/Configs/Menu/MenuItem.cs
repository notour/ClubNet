namespace ClubNet.WebSite.Domain.Configs.Menu
{
    using ClubNet.WebSite.Domain.Security;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a menu item
    /// </summary>
    /// <seealso cref="ClubNet.WebSite.Domain.Entity{ClubNet.WebSite.Domain.Configs.ConfigType}" />
    [DataContract]
    public abstract class MenuItem : Entity<ConfigType>
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        protected MenuItem(Guid id, LocalizedString label, LocalizedString description, ConfigType entityType, SecurityCriteria securityCriteria)
            : base(id, entityType, securityCriteria)

        {
            Label = label;
            Description = description;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu item name
        /// </summary>
        [DataMember]
        public string Name { get; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        [DataMember]
        public LocalizedString Label { get; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public LocalizedString Description { get; }

        #endregion
    }
}
