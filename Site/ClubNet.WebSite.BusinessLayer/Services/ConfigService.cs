using ClubNet.WebSite.BusinessLayer.Configurations;
using ClubNet.WebSite.Common.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubNet.WebSite.BusinessLayer.Services
{
    /// <summary>
    /// Define the <see cref="IConfigService"/> implementations
    /// </summary>
    class ConfigService : IConfigService
    {
        #region Fields

        private readonly IOptions<DefaultConfiguration> _defaultConfiguration;

        #endregion

        #region Ctor

        public ConfigService(IOptions<DefaultConfiguration> defaultConfiguration)
        {
            _defaultConfiguration = defaultConfiguration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default theme name
        /// </summary>
        public string DefaultTheme
        {
            get { return _defaultConfiguration.Value.Theme; }
        }
     
        #endregion
    }
}
