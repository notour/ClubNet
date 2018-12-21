using ClubNet.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

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

        #endregion
    }
}
