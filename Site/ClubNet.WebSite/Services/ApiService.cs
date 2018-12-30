namespace ClubNet.WebSite.Services
{
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using System.Collections.Immutable;
    using System.Linq;

    /// <summary>
    /// Managed the api access
    /// </summary>
    sealed class ApiService : IApiService
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
            _apiKeyProviders = ImmutableArray<IApiKeyProvider>.Empty;

            if (clubDescriptor != null && clubDescriptor.ClubApiKeyProvider != null)
                _apiKeyProviders = _apiKeyProviders.Add(clubDescriptor.ClubApiKeyProvider);

            if (configService != null && configService.ApiKeyProvider != null)
                _apiKeyProviders = _apiKeyProviders.Add(configService.ApiKeyProvider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the api public key
        /// </summary>
        public string GetPublicApiKey(Apis api)
        {
            foreach (var apiProvider in _apiKeyProviders)
            {
                var key = apiProvider.GetApiPublicKey(api);
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
            foreach (var apiProvider in _apiKeyProviders)
            {
                var key = apiProvider.GetApiPrivateKey(api);
                if (!string.IsNullOrEmpty(key))
                    return key;
            }

            return string.Empty;
        }

        #endregion
    }
}
