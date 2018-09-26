using System;

namespace ClubNet.Framework.Attributes
{
    /// <summary>
    /// Flag a structure that contains configuration elements
    /// </summary>
    public class ConfigurationDataAttribute : Attribute
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ConfigurationDataAttribute"/>
        /// </summary>
        public ConfigurationDataAttribute(string sectionKey)
        {
            SectionKey = sectionKey;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Section key dedicated to the current configuration  object
        /// </summary>
        public string SectionKey { get; }

        #endregion
    }
}
