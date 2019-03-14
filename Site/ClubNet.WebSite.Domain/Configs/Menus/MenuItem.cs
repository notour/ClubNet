namespace ClubNet.WebSite.Domain.Configs.Menus
{
    using System.Runtime.Serialization;

    using ClubNet.WebSite.Domain.Security;

    using MongoDB.Bson.Serialization.Attributes;

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
        public LocalizedString Label { get; private set; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        [BsonElement]
        [BsonIgnoreIfDefault]
        [DataMember(EmitDefaultValue = false)]
        public LocalizedString Description { get; private set; }

        /// <summary>
        /// Gets the Glyphicon
        /// </summary>
        [BsonElement]
        [DataMember]
        public string Glyphicon { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Create(string name, LocalizedString label, string glyphicon, LocalizedString description, SecurityCriteria securityCriteria, bool isDraft)
        {
            SetData(name, label, glyphicon, description);
            base.Create(securityCriteria, isDraft);
        }

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Update(string name, LocalizedString label, string glyphicon, LocalizedString description, SecurityCriteria securityCriteria, bool isDraft)
        {
            SetData(name, label, glyphicon, description);
            base.Update(securityCriteria, isDraft);
        }

        /// <summary>
        /// Initialize the entity data
        /// </summary>
        private void SetData(string name, LocalizedString label, string glyphicon, LocalizedString description)
        {
            Name = name;
            Label = label;
            Description = description;
            Glyphicon = glyphicon;
        }

        #endregion
    }
}
