namespace ClubNet.WebSite.Middleware
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Domain.User;

    using Microsoft.AspNetCore.Http;

    public class LocalizedUrlMiddleware
    {
        #region Fields

        private static readonly Regex s_languageUrl;

        private readonly IConfigService _configService;
        private readonly RequestDelegate _next;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="LocalizedUrlMiddleware"/>
        /// </summary>
        static LocalizedUrlMiddleware()
        {
            s_languageUrl = new Regex("(^(/[a-zA-Z]{2}/))|(^(/[a-zA-Z]{2})$)");
        }

        /// <summary>
        /// Initialize a new instance of the class <see cref="LocalizedUrlMiddleware"/>
        /// </summary>
        public LocalizedUrlMiddleware(RequestDelegate next, IConfigService configService)
        {
            this._next = next;
            this._configService = configService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called to execute the current middle ware
        /// </summary>
        public async Task Invoke(HttpContext httpContext)
        {
            if (!s_languageUrl.IsMatch(httpContext.Request.Path))
            {
                string lang = this._configService.DefaultLanguage.TwoLetterISOLanguageName;
                if (httpContext.User != null)
                {
                    var userPreferedCultureClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == nameof(UserInfo.PreferredCulture));
                    if (userPreferedCultureClaim != null)
                        lang = userPreferedCultureClaim.Value;
                }
                var newUrl = ($"/{lang}/" + httpContext.Request.Path).Replace("//", "/");

                if (httpContext.Request.QueryString != null && httpContext.Request.QueryString.HasValue)
                    newUrl += "?" + httpContext.Request.QueryString;

                httpContext.Response.Redirect(newUrl);
                return;
            }
            await this._next(httpContext);
        }

        #endregion
    }
}
