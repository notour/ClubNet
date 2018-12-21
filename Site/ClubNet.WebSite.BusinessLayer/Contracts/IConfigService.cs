using ClubNet.WebSite.Domain.Configs.Menu;
using System;

namespace ClubNet.WebSite.BusinessLayer.Contracts
{
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

        #endregion
    }
}
