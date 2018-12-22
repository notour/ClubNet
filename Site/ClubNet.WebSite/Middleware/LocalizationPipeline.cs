using ClubNet.WebSite.BusinessLayer.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ClubNet.WebSite.Middleware
{
    public class LocalizationPipeline
    {
        public void Configure(IApplicationBuilder app, IOptions<DefaultConfiguration> defaultConfig)
        {
            var options = new RequestLocalizationOptions()
            {

                DefaultRequestCulture = new RequestCulture(culture: defaultConfig.Value.DefaultLanguage.TwoLetterISOLanguageName, 
                                                           uiCulture: defaultConfig.Value.DefaultLanguage.TwoLetterISOLanguageName),
                SupportedCultures = defaultConfig.Value.MangagedLanguage,
                SupportedUICultures = defaultConfig.Value.MangagedLanguage,
            };

            options.RequestCultureProviders = new[] { new RouteDataRequestCultureProvider() { Options = options, RouteDataStringKey = "lang", UIRouteDataStringKey = "lang" } };

            app.UseRequestLocalization(options);
        }
    }
}
