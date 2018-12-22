namespace ClubNet.WebSite.Tools
{
    using ClubNet.WebSite.BusinessLayer.Configurations;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Constraint used to filter the language parameter
    /// </summary>
    public class LanguageRouteConstraint : IRouteConstraint
    {
        #region Ctor

        /// <summary>
        /// Initiaze a new instance of the class <see cref="LanguageRouteConstraint"/>
        /// </summary>
        public LanguageRouteConstraint(IOptions<DefaultConfiguration> defaultConfig)
        {
            // https://gunnarpeipman.com/aspnet/aspnet-core-simple-localization/
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

            return lang == "en" || lang == "en" || lang == "ru";
        }

        #endregion
    }
}
