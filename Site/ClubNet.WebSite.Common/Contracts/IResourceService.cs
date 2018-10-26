using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ClubNet.WebSite.Common.Errors;

namespace ClubNet.WebSite.Common.Contracts
{
    /// <summary>
    /// Resource service used to provide thought all the application the access to resource configured in function of the customer
    /// </summary>
    public interface IResourceService
    {
        #region Methods

        /// <summary>
        /// Gets an error string localized 
        /// </summary>
        string GetString(ErrorCategory category, string errorKey, CultureInfo cultureInfo = null);

        #endregion
    }
}
