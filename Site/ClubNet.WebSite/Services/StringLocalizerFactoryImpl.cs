namespace ClubNet.WebSite.Services
{
    using System;
    using System.Collections.Immutable;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Localization;

    /// <summary>
    /// Define a factory to produce <see cref="IStringLocalizer"/>
    /// </summary>
    internal class StringLocalizerFactoryImpl : IStringLocalizerFactory
    {
        #region Fields

        private IImmutableDictionary<Type, StringLocalizerImpl> _localizerCache;
        private readonly IHttpContextAccessor _contextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="StringLocalizerFactoryImpl"/>
        /// </summary>
        public StringLocalizerFactoryImpl(IHttpContextAccessor contextAccessor)
        {
            this._localizerCache = ImmutableDictionary<Type, StringLocalizerImpl>.Empty;
            this._contextAccessor = contextAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create or get the <see cref="IStringLocalizer"/> in function of the resource Type
        /// </summary>
        public IStringLocalizer Create(Type resourceSource)
        {
            lock (this._localizerCache)
            {
                StringLocalizerImpl stringLocalizer = null;
                if (!this._localizerCache.TryGetValue(resourceSource, out stringLocalizer))
                {
                    stringLocalizer = new StringLocalizerImpl(this, resourceSource, this._contextAccessor);
                    this._localizerCache = this._localizerCache.Add(resourceSource, stringLocalizer);
                }

                return stringLocalizer;
            }
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
