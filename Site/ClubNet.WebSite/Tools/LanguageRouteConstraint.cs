namespace ClubNet.WebSite.Tools
{
    using System.Linq;
    using ClubNet.WebSite.Common.Contracts;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// Constraint used to filter the language parameter
    /// </summary>
    public class LanguageRouteConstraint : IRouteConstraint
    {
        #region Fields

        private readonly IConfigService _defaultConfig;

        #endregion

        #region Ctor

        /// <summary>
        /// Initiaze a new instance of the class <see cref="LanguageRouteConstraint"/>
        /// </summary>
        public LanguageRouteConstraint(IConfigService defaultConfig)
        {
            this._defaultConfig = defaultConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Match the specific language
        /// </summary>
        /// <returns></returns>
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey("lang"))
            {
                return false;
            }

            var lang = values["lang"].ToString();

            if (this._defaultConfig.ManagedLanguage.Any(l => string.Equals(l.TwoLetterISOLanguageName, lang, System.StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        }

        #endregion
    }
}
