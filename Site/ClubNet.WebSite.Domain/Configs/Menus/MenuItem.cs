namespace ClubNet.WebSite.Domain.Configs.Menus
{
    using ClubNet.WebSite.Domain.Security;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a menu item
    /// </summary>
    /// <seealso cref="Domain.Entity{ConfigType}" />
    [BsonKnownTypes(typeof(Menu), typeof(MenuLinkItem))]
    [DataContract]
    public abstract class MenuItem : Entity<ConfigType>
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        protected MenuItem(ConfigType configType)
            : base(configType)

        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu item name
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [DataMember]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        [BsonElement]
        [BsonIgnoreIfDefault]
        [DataMember(EmitDefaultValue = false)]
        public LocalizedString Label { get; private set;}

        /// <summary>
        /// Gets the label.
        /// </summary>
        [BsonElement]
        [BsonIgnoreIfDefault]
        [DataMember(EmitDefaultValue = false)]
        public LocalizedString Description { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Create(string name, LocalizedString label, LocalizedString description, SecurityCriteria securityCriteria)
        {
            SetData(name, label, description);
            base.Create(securityCriteria);
        }

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Update(string name, LocalizedString label, LocalizedString description, SecurityCriteria securityCriteria)
        {
            SetData(name, label, description);
            base.Update(securityCriteria);
        }

        /// <summary>
        /// Initialize the entity data
        /// </summary>
        private void SetData(string name, LocalizedString label, LocalizedString description)
        {
            Name = name;
            Label = label;
            Description = description;
        }

        #endregion
    }
}
