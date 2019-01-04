namespace ClubNet.WebSite.BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using ClubNet.WebSite.BusinessLayer.Configurations;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Common.Tools;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Define the <see cref="IConfigService"/> implementations
    /// </summary>
    internal class ConfigService : IConfigService
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
            this._defaultConfiguration = defaultConfiguration;

            var keys = configuration.GetValue<Dictionary<Apis, ApiKeys>>(nameof(Apis));

            if (keys != null)
                this.ApiKeyProvider = new ApiKeyProvider(keys);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default theme name
        /// </summary>
        public string DefaultTheme
        {
            get { return this._defaultConfiguration.Value.Theme; }
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
            get { return this._defaultConfiguration.Value.DefaultLanguage; }
        }

        /// <summary>
        /// Gets the list of all the managed languaged
        /// </summary>
        public IReadOnlyCollection<CultureInfo> ManagedLanguage
        {
            get { return (IReadOnlyCollection<CultureInfo>)this._defaultConfiguration.Value.MangagedLanguage; }
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
