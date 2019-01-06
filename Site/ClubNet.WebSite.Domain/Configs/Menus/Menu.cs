namespace ClubNet.WebSite.Domain.Configs.Menus
{
    using ClubNet.WebSite.Domain.Security;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a specific menu
    /// </summary>
    /// <seealso cref="Menu" />
    [BsonDiscriminator(Required = true)]
    [DataContract]
    public sealed class Menu : MenuItem
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        public Menu()
            : base(ConfigType.Menu)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu items
        /// </summary>
        [DataMember]
        public IEnumerable<MenuItem> Items { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new <see cref="Menu"/>
        /// </summary>
        public static Menu Create(string name, LocalizedString label, string glyphicon, LocalizedString description, IEnumerable<MenuItem> items, SecurityCriteria securityCriteria)
        {
            var inst = new Menu();
            inst.Items = items;
            inst.Create(name, label, glyphicon, description, securityCriteria);

            return inst;
        }

        /// <summary>
        /// Create a new <see cref="Menu"/>
        /// </summary>
        public void Update(string name, LocalizedString label, string glyphicon, LocalizedString description, IEnumerable<MenuItem> items, SecurityCriteria securityCriteria)
        {
            this.Items = items;
            this.Update(name, label, glyphicon, description, securityCriteria);
        }

        #endregion
    }
}
