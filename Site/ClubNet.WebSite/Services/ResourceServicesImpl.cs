namespace ClubNet.WebSite.Services
{
    using System;
    using System.Globalization;

    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Errors;
    using ClubNet.WebSite.Resources;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Localization;

    /// <summary>
    /// Implement the resource service
    /// </summary>
    internal class ResourceServicesImpl : IResourceService
    {
        #region Fields

        public const string ERROR_PREFIX = "Err_";
        public const string DEFAULT = "Default";

        public const string MISSING_RESOURCE = "Missing resource in Resx {0} - key : {1}";
        public const string MISSING_RESOURCE_APPEND_CONTEXT = " - Context : {0}";

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IStringLocalizer<ErrorMessages> _errorMessages;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="ResourceServicesImpl"/>
        /// </summary>
        static ResourceServicesImpl()
        {
        }

        /// <summary>
        /// Initialize a new instance of the class <see cref="ResourceServicesImpl"/>
        /// </summary>
        public ResourceServicesImpl(IHttpContextAccessor contextAccessor, IStringLocalizer<ErrorMessages> errorMessages)
        {
            this._contextAccessor = contextAccessor;
            this._errorMessages = errorMessages;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the localized formated string
        /// </summary>
        public string GetString(ErrorCategory category, string errorKey, CultureInfo culture = null)
        {
            if (culture == null)
                culture = this._contextAccessor.CurrentRequestService().CurrentLanguage;

            string[] arguments = null;

            if (string.IsNullOrEmpty(errorKey))
                errorKey = category.ToString() + "_" + DEFAULT;
            else
            {
                string[] parts = errorKey.Split(":".ToCharArray(), 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    arguments = parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    errorKey = parts[0].Trim();
                }
            }

            var resource = this._errorMessages.GetString(errorKey, arguments);
            if (resource != null)
                return resource;

            return string.Format(MISSING_RESOURCE, category.ToString(), errorKey) + MISSING_RESOURCE_APPEND_CONTEXT;
        }

        /// <summary>
        /// Get the localized formated string
        /// </summary>
        public string GetString(string globalKey, CultureInfo cultureInfo = null)
        {
            return GetString(ErrorCategory.None, globalKey, cultureInfo);
        }

        #endregion
    }
}
