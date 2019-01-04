namespace ClubNet.WebSite.Services
{
    using System.Collections.Immutable;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;

    /// <summary>
    /// Managed the api access
    /// </summary>
    internal sealed class ApiService : IApiService
    {
        #region Fields

        private readonly ImmutableArray<IApiKeyProvider> _apiKeyProviders;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ApiService"/>
        /// </summary>
        public ApiService(IClubDescriptor clubDescriptor, IConfigService configService)
        {
            this._apiKeyProviders = ImmutableArray<IApiKeyProvider>.Empty;

            if (clubDescriptor != null && clubDescriptor.ClubApiKeyProvider != null)
                this._apiKeyProviders = this._apiKeyProviders.Add(clubDescriptor.ClubApiKeyProvider);

            if (configService != null && configService.ApiKeyProvider != null)
                this._apiKeyProviders = this._apiKeyProviders.Add(configService.ApiKeyProvider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the api public key
        /// </summary>
        public string GetPublicApiKey(Apis api)
        {
            foreach (var apiProvider in this._apiKeyProviders)
            {
                string key = apiProvider.GetApiPublicKey(api);
                if (!string.IsNullOrEmpty(key))
                    return key;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the api private key
        /// </summary>
        public string GetPrivateApiKey(Apis api)
        {
            foreach (var apiProvider in this._apiKeyProviders)
            {
                string key = apiProvider.GetApiPrivateKey(api);
                if (!string.IsNullOrEmpty(key))
                    return key;
            }

            return string.Empty;
        }

        #endregion
    }
}
