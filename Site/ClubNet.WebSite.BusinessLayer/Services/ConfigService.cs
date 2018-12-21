﻿using ClubNet.WebSite.BusinessLayer.Configurations;
using ClubNet.WebSite.BusinessLayer.Contracts;
using ClubNet.WebSite.Common.Contracts;
using ClubNet.WebSite.Domain.Configs.Menu;
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

        private static readonly TimeSpan s_defaultTimeout;

        private readonly IOptions<DefaultConfiguration> _defaultConfiguration;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="ConfigService"/>
        /// </summary>
        static ConfigService()
        {
            s_defaultTimeout = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Initialize a new instance of the class <see cref="ConfigService"/>
        /// </summary>
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

        /// <summary>
        /// Gets the default application timeout
        /// </summary>
        public TimeSpan DefaultTimeout
        {
            get { return s_defaultTimeout; }
        }

        #endregion
    }
}
