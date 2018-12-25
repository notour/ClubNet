namespace ClubNet.WebSite.Services
{
    using ClubNet.WebSite.Resources;
    using ClubNet.WebSite.Resources.Controllers;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Localization;

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Define a string localizer from the specific controller resource file, global one and DB
    /// </summary>
    class StringLocalizerImpl : IStringLocalizer
    {
        #region Fields

        private static readonly ResourceManager s_sharedResourceManager;
        private static readonly HashSet<string> s_sharedResourceKeys;

        private readonly HashSet<string> _resourceStringsKeys;
        private readonly ResourceManager _resourceManager;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly CultureInfo _fixedCulture;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="StringLocalizerImpl"/>
        /// </summary>
        static StringLocalizerImpl()
        {
            s_sharedResourceManager = SharedResources.ResourceManager;
            s_sharedResourceKeys = ExtractResourceKeys(typeof(SharedResources));
        }

        /// <summary>
        /// Initialize a new instance of the class <see cref="StringLocalizerImpl"/>
        /// </summary>
        public StringLocalizerImpl(IStringLocalizerFactory stringLocalizerFactory, Type resourceType, IHttpContextAccessor contextAccessor)
        {
            this._stringLocalizerFactory = stringLocalizerFactory;
            this._contextAccessor = contextAccessor;
            var resourceDotNetType = Type.GetType("ClubNet.WebSite.Resources.Controllers." + resourceType.Name);

            this._resourceManager = new ResourceManager(resourceDotNetType.FullName, typeof(StringLocalizerImpl).Assembly);
            this._resourceStringsKeys = ExtractResourceKeys(resourceDotNetType);
        }

        /// <summary>
        /// Initialize a new instance of the class <see cref="StringLocalizerImpl"/>
        /// </summary>
        public StringLocalizerImpl(IStringLocalizerFactory stringLocalizerFactory, Type resourceType, CultureInfo fixedCulture)
           : this(stringLocalizerFactory, resourceType, (IHttpContextAccessor)null)
        {
            this._fixedCulture = fixedCulture;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="LocalizedString"/> based on the resource key
        /// </summary>
        public LocalizedString this[string name]
        {
            get
            {
                string value = GetResourceValue(name);
                return new LocalizedString(name, value == null ? string.Empty : value, value == null);
            }
        }

        /// <summary>
        /// Gets the <see cref="LocalizedString"/> based on the resource key
        /// </summary>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string value = GetResourceValue(name);
                return new LocalizedString(name, value != null ? string.Format(value, arguments) : string.Empty, value == null);
            }
        }

        #endregion

        #region Methods

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the string localizer specialized on a fixed culture
        /// </summary>
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new StringLocalizerImpl(_stringLocalizerFactory, _resourceManager.ResourceSetType, culture);
        }

        #region Tools

        /// <summary>
        /// Gets the current value by name
        /// </summary>
        private string GetResourceValue(string name)
        {
            var culture = GetCurrentCulture();
            string value = null;

            if (this._resourceStringsKeys.Contains(name))
                value = this._resourceManager.GetString(name, culture);

            if (s_sharedResourceKeys.Contains(name))
                value = s_sharedResourceManager.GetString(name, GetCurrentCulture());

            return value;
        }

        /// <summary>
        /// Gets the current culture
        /// </summary>
        private CultureInfo GetCurrentCulture()
        {
            if (this._fixedCulture != null)
                return this._fixedCulture;
            var reqCultureService = this._contextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            return reqCultureService.RequestCulture.Culture;
        }

        #region Tools

        /// <summary>
        /// Extract the resource name keys
        /// </summary>
        private static HashSet<string> ExtractResourceKeys(Type resourceDotNetType)
        {
            return new HashSet<string>(resourceDotNetType.GetProperties(BindingFlags.Static | BindingFlags.NonPublic)
                                                         .Where(p => p.PropertyType == typeof(string))
                                                         .Select(p => p.Name)
                                                         .ToArray());
        }

        #endregion

        #endregion

        #endregion
    }
}
