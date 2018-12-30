using ClubNet.WebSite.Common.Enums;

namespace ClubNet.WebSite.Common.Contracts
{
    /// <summary>
    /// Define a service available to provide api keys
    /// </summary>
    public interface IApiKeyProvider
    {
        #region Methods

        /// <summary>
        /// Gets the api public key
        /// </summary>
        string GetApiPublicKey(Apis api);

        #endregion
    }
}
