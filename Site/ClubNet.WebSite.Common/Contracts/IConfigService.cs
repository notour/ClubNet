namespace ClubNet.WebSite.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Define a configuration
    /// </summary>
    public interface IConfigService
    {
        #region Properties

        /// <summary>
        /// Gets the current project id
        /// </summary>
        Guid ProjectId { get; }

        /// <summary>
        /// Gets the default theme name
        /// </summary>
        string DefaultTheme { get; }

        /// <summary>
        /// Gets the current project content folder
        /// </summary>
        string ProjectContentFolder { get; }

        /// <summary>
        /// Gets the default default timeout
        /// </summary>
        TimeSpan DefaultRequestTimeout { get; }

        /// <summary>
        /// Gets the default language 
        /// </summary>
        CultureInfo DefaultLanguage { get; }

        /// <summary>
        /// Gets all the managed languaged
        /// </summary>
        IReadOnlyCollection<CultureInfo> ManagedLanguage { get; }

        /// <summary>
        /// Gets the club api key provider
        /// </summary>
        IApiKeyProvider ApiKeyProvider { get; }

        #endregion
    }
}
