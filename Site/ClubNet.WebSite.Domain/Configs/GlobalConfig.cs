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
        public GlobalConfig(Guid id, ConfigType entityType, string theme)
            : base(id, entityType)
        {
            Theme = theme;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the theme name
        /// </summary>
        [DataMember]
        public string Theme { get; }

        #endregion

    }
}
