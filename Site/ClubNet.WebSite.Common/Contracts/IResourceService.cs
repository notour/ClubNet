namespace ClubNet.WebSite.Common.Contracts
{
    using ClubNet.WebSite.Common.Errors;

    using System.Globalization;

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
