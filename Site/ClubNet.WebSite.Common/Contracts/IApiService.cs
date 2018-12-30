namespace ClubNet.WebSite.Common.Contracts
{
    using ClubNet.WebSite.Common.Enums;

    /// <summary>
    /// Define a service user to communicate with other api
    /// </summary>
    public interface IApiService
    {
        #region Methods

        /// <summary>
        /// Gets the public key of the specific api 
        /// </summary>
        string GetPublicApiKey(Apis api);

        #endregion
    }
}
