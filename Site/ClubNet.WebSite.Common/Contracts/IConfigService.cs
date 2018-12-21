using System;
using System.Collections.Generic;
using System.Text;

namespace ClubNet.WebSite.Common.Contracts
{
    /// <summary>
    /// Define a configuration
    /// </summary>
    public interface IConfigService
    {
        #region Properties

        /// <summary>
        /// Get the default theme name
        /// </summary>
        string DefaultTheme { get; }

        #endregion
    }
}
