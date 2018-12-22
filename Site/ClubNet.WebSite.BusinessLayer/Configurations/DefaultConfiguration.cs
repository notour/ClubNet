using ClubNet.Framework.Attributes;
using System.Collections.Generic;
using System.Globalization;

namespace ClubNet.WebSite.BusinessLayer.Configurations
{
    /// <summary>
    /// Define the default site configuration
    /// </summary>
    [ConfigurationData(ConfigurationSectionKey)]
    public sealed class DefaultConfiguration
    {
        #region Fields

        public const string ConfigurationSectionKey = "DefaultConfig";

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="DefaultConfiguration"/>
        /// </summary>
        public DefaultConfiguration()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default theme name
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Gets the default language 
        /// </summary>
        public CultureInfo DefaultLanguage { get; set; }

        /// <summary>
        /// Gets all the managed languaged
        /// </summary>
        public IList<CultureInfo> MangagedLanguage { get; set; }

        #endregion
    }
}
