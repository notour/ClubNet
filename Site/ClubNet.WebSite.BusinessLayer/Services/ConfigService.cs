namespace ClubNet.WebSite.BusinessLayer.Services
{
    using ClubNet.WebSite.BusinessLayer.Configurations;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Common.Tools;
    using ClubNet.WebSite.Domain.Configs.Menus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

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
        public ConfigService(IOptions<DefaultConfiguration> defaultConfiguration, IConfiguration configuration)
        {
            _defaultConfiguration = defaultConfiguration;

            var keys = configuration.GetValue<Dictionary<Apis, ApiKeys>>(nameof(Apis));

            if (keys != null)
                ApiKeyProvider = new ApiKeyProvider(keys);
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
        public TimeSpan DefaultRequestTimeout
        {
            get { return s_defaultTimeout; }
        }

        /// <summary>
        /// Gets the default languaged configured
        /// </summary>
        public CultureInfo DefaultLanguage
        {
            get { return _defaultConfiguration.Value.DefaultLanguage; }
        }

        /// <summary>
        /// Gets the list of all the managed languaged
        /// </summary>
        public IReadOnlyCollection<CultureInfo> MangagedLanguage
        {
            get { return (IReadOnlyCollection<CultureInfo>)_defaultConfiguration.Value.MangagedLanguage; }
        }

        public Guid ProjectId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string ProjectContentFolder
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the global api keys
        /// </summary>
        public IApiKeyProvider ApiKeyProvider { get; }

        #endregion
    }
}
