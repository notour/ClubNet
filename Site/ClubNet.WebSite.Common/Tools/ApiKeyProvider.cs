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

        private const string PrivateKey = "Private";
        private const string PublicKey = "Public";

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
                                               new KeyValuePair<string, string>(kv.Key.ToString() + "_" + PublicKey, kv.Value.Public),
                                               new KeyValuePair<string, string>(kv.Key.ToString() + "_" + PrivateKey, kv.Value.Private)
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
            if (_apiKeyStore.TryGetValue((api.ToString() + "_" + PublicKey).ToUpperInvariant(), out var key))
                return key;
            return string.Empty;
        }

        #endregion
    }
}
