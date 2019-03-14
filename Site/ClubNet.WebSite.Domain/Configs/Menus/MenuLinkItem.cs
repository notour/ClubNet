namespace ClubNet.WebSite.Domain.Configs.Menus
{
    using System.Runtime.Serialization;

    using ClubNet.WebSite.Domain.Security;

    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Define a menu item used to navigate to a specific link
    /// </summary>
    /// <seealso cref="MenuItem" />
    [BsonDiscriminator(Required = true)]
    [DataContract]
    public sealed class MenuLinkItem : MenuItem
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLinkItem"/> class.
        /// </summary>
        public MenuLinkItem()
            : base(ConfigType.MenuLink)

        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the controller name.
        /// </summary>
        [BsonIgnoreIfNull]
        [DataMember(EmitDefaultValue = false)]
        public string Controller { get; private set; }

        /// <summary>
        /// Gets the controller action
        /// </summary>
        [BsonIgnoreIfNull]
        [DataMember(EmitDefaultValue = false)]
        public string Action { get; private set; }

        /// <summary>
        /// Gets the direct URL
        /// </summary>
        [BsonIgnoreIfNull]
        [DataMember(EmitDefaultValue = false)]
        public string Url { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new <see cref="Menu"/>
        /// </summary>
        public static MenuLinkItem Create(string name,
                                            LocalizedString label,
                                            string glyphicon,
                                            LocalizedString description,
                                            string controller,
                                            string action,
                                            string url,
                                            SecurityCriteria securityCriteria,
                                            bool isDraft)
        {
            var inst = new MenuLinkItem();
            SetData(controller, action, url, inst);
            inst.Create(name, label, glyphicon, description, securityCriteria, isDraft);

            return inst;
        }

        /// <summary>
        /// Create a new <see cref="Menu"/>
        /// </summary>
        public void Update(string name,
                           LocalizedString label,
                           string glyphicon,
                           LocalizedString description,
                           string controller,
                           string action,
                           string url,
                           SecurityCriteria securityCriteria,
                           bool isDraft)
        {
            SetData(controller, action, url, this);
            this.Update(name, label, glyphicon, description, securityCriteria, isDraft);
        }

        /// <summary>
        /// Set linked menu
        /// </summary>
        private static void SetData(string controller, string action, string url, MenuLinkItem inst)
        {
            inst.Controller = controller;
            inst.Action = action;
            inst.Url = url;
        }


        #endregion

    }
}
