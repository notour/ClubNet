namespace ClubNet.Phoenix.Configuration
{
    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Common.Tools;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration for the phoenix
    /// </summary>
    [DataContract]
    class PhoenixConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets the club display name
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the main logo path
        /// </summary>
        [DataMember]
        public Dictionary<SiteResources, string> Media { get; set; }

        /// <summary>
        /// Gets or sets the club api keys
        /// </summary>
        [DataMember]
        public Dictionary<Apis, ApiKeys> ApiKeys { get; set; }

        #endregion
    }
}
