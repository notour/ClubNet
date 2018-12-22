namespace ClubNet.WebSite.BusinessLayer.Contracts
{
    using ClubNet.WebSite.Domain.Configs.Menus;
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
        /// Gets the default theme name
        /// </summary>
        string DefaultTheme { get; }

        /// <summary>
        /// Gets the default default timeout
        /// </summary>
        TimeSpan DefaultTimeout { get; }

        /// <summary>
        /// Gets the default language 
        /// </summary>
        CultureInfo DefaultLanguage { get; }

        /// <summary>
        /// Gets all the managed languaged
        /// </summary>
        IReadOnlyCollection<CultureInfo> MangagedLanguage { get; }

        #endregion
    }
}
