namespace ClubNet.WebSite.Common.Tools
{
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;

    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    /// <summary>
    /// Define an implementation of the api key provider
    /// </summary>
    public sealed class ApiKeyProvider : IApiKeyProvider
    {
        #region Fields

        private readonly IReadOnlyDictionary<string, string> _apiKeyStore;

        private const string PRIVATE_KEY = "Private";
        private const string PUBLIC_KEY = "Public";

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ApiKeyProvider"/>
        /// </summary>
        public ApiKeyProvider(IReadOnlyDictionary<Apis, ApiKeys> apiKeys)
        {
            this._apiKeyStore = apiKeys.SelectMany(kv =>
                                       {
                                           return new KeyValuePair<string, string>[]
                                           {
                                               new KeyValuePair<string, string>(kv.Key.ToString() + "_" + PUBLIC_KEY, kv.Value.Public),
                                               new KeyValuePair<string, string>(kv.Key.ToString() + "_" + PRIVATE_KEY, kv.Value.Private)
                                           };
                                       })
                                       .ToImmutableDictionary(k => k.Key.ToUpperInvariant(), v => v.Value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the api public key
        /// </summary>
        public string GetApiPublicKey(Apis api)
        {
            if (this._apiKeyStore.TryGetValue((api.ToString() + "_" + PUBLIC_KEY).ToUpperInvariant(), out string key))
                return key;
            return string.Empty;
        }

        /// <summary>
        /// Get the api private key
        /// </summary>
        public string GetApiPrivateKey(Apis api)
        {
            if (this._apiKeyStore.TryGetValue((api.ToString() + "_" + PRIVATE_KEY).ToUpperInvariant(), out string key))
                return key;
            return string.Empty;
        }

        #endregion
    }
}
