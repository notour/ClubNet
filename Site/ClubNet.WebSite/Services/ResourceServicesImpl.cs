namespace ClubNet.WebSite.Services
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Globalization;
    using System.Resources;

    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Errors;
    using ClubNet.WebSite.Resources;

    /// <summary>
    /// Implement the resource service
    /// </summary>
    class ResourceServicesImpl : IResourceService
    {
        #region Fields

        public const string ERROR_PREFIX = "Err_";
        public const string DEFAULT = "Default";

        public const string MISSING_RESOURCE = "Missing resource in Resx {0} - key : {1}";
        public const string MISSING_RESOURCE_APPEND_CONTEXT = " - Context : {0}";

        private static readonly IImmutableDictionary<string, ResourceManager> s_indexedResources;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="ResourceServicesImpl"/>
        /// </summary>
        static ResourceServicesImpl()
        {
            var indexedResources = new Dictionary<string, ResourceManager>();

            indexedResources.Add(ErrorCategory.User.ToString(), UserRes.ResourceManager);
            indexedResources.Add(ErrorCategory.Logged.ToString(), GlobalRes.ResourceManager);

            s_indexedResources = indexedResources.ToImmutableDictionary();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the localized formated string
        /// </summary>
        public string GetString(ErrorCategory category, string errorKey, CultureInfo culture = null)
        {
            if (s_indexedResources.TryGetValue(category.ToString(), out var resourceManager))
            {
                if (culture == null)
                    culture = System.Threading.Thread.CurrentThread.CurrentUICulture;

                if (string.IsNullOrEmpty(errorKey))
                    errorKey = category.ToString() + "_" + DEFAULT;
                
                var resource = resourceManager.GetString(ERROR_PREFIX + errorKey, culture);
                if (resource != null)
                    return resource;
            }
            return string.Format(MISSING_RESOURCE, category.ToString(), errorKey) + MISSING_RESOURCE_APPEND_CONTEXT;
        }

        #endregion
    }
}
