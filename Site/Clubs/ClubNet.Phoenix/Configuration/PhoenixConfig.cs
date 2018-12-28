namespace ClubNet.Phoenix.Configuration
{
    using ClubNet.WebSite.Common.Enums;
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
        /// Gets or sets the main logo path
        /// </summary>
        [DataMember]
        public Dictionary<SiteResources, string> Media { get; set; }

        #endregion
    }
}
