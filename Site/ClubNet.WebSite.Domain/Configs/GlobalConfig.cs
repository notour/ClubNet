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

        #endregion

        #region Methods

        /// <summary>
        /// Create a new <see cref="GlobalConfig"/>
        /// </summary>
        public static GlobalConfig Create(string theme, SecurityCriteria securityCriteria)
        {
            var cfg = new GlobalConfig();
            cfg.ImplCreate(theme, securityCriteria);
            return cfg;
        }

        /// <summary>
        /// Update the current entity
        /// </summary>
        private void ImplCreate(string theme, SecurityCriteria securityCriteria)
        {
            SetData(theme);
            base.Create(securityCriteria);
        }

        /// <summary>
        /// Update the current entity
        /// </summary>
        public void Update(string theme, SecurityCriteria securityCriteria)
        {
            SetData(theme);
            base.Update(securityCriteria);
        }

        /// <summary>
        /// Initialize the entity data
        /// </summary>
        private void SetData(string theme)
        {
            this.Theme = theme;
        }

        #endregion

    }
}
