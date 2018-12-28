using ClubNet.WebSite.Domain.Security;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ClubNet.WebSite.Domain.Configs
{
    /// <summary>
    /// Model that group all the basic configuration of a web site
    /// </summary>
    [DataContract]
    public sealed class GlobalConfig : Entity<ConfigType>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="GlobalConfig"/>
        /// </summary>
        public GlobalConfig()
            : base(ConfigType.Global)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the theme name
        /// </summary>
        [DataMember]
        public string Theme { get; private set; }

        /// <summary>
        /// Gets the configuration public name
        /// </summary>
        [DataMember]
        public string ConfigName { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new <see cref="GlobalConfig"/>
        /// </summary>
        public static GlobalConfig Create(string theme, string configName, SecurityCriteria securityCriteria)
        {
            var cfg = new GlobalConfig();
            cfg.ImplCreate(theme, configName, securityCriteria);
            return cfg;
        }

        /// <summary>
        /// Update the current entity
        /// </summary>
        private void ImplCreate(string theme, string configName, SecurityCriteria securityCriteria)
        {
            SetData(theme, configName);
            base.Create(securityCriteria);
        }

        /// <summary>
        /// Update the current entity
        /// </summary>
        public void Update(string theme, string configName, SecurityCriteria securityCriteria)
        {
            SetData(theme, configName);
            base.Update(securityCriteria);
        }

        /// <summary>
        /// Initialize the entity data
        /// </summary>
        private void SetData(string theme, string configName)
        {
            this.Theme = theme;
            this.ConfigName = configName;
        }

        #endregion

    }
}
