namespace ClubNet.WebSite.Tools
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using Microsoft.AspNetCore.Mvc.Razor;

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Expand the view location to clud the theme systems
    /// </summary>
    class TenantViewLocationExpander : IViewLocationExpander
    {
        #region Fields

        private const string THEME_KEY = "theme";
        private const string CLUB_KEY = "clubPath";

        #endregion

        #region Methods

        /// <summary>
        /// Add the theme key into the http context
        /// </summary>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[THEME_KEY] = ((IThemeBL)context.ActionContext.HttpContext.RequestServices.GetService(typeof(IThemeBL)))?.CurrentThemeName;

            var club = ((IClubDescriptor)context.ActionContext.HttpContext.RequestServices.GetService(typeof(IClubDescriptor)));
            if (club != null)
                context.Values[CLUB_KEY] = club.GetType().Namespace.Replace(".", "/");
        }

        /// <summary>
        /// Append view location resolver path
        /// </summary>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.Values.TryGetValue(THEME_KEY, out var theme) && !string.IsNullOrEmpty(theme))
            {
                viewLocations = new[] {
                    $"/Themes/{theme}/{{1}}/{{0}}.cshtml",
                    $"/Themes/{theme}/Shared/{{0}}.cshtml",
                }
                .Concat(viewLocations).ToArray();
            }

            if (context.Values.TryGetValue(CLUB_KEY, out var club) && !string.IsNullOrEmpty(club))
            {
                viewLocations = new[] {
                    $"{club}/Views/{{1}}/{{0}}.cshtml",
                    $"{club}/Views/Shared/{{0}}.cshtml",
                    $"{club}/Themes/{theme}/{{1}}/{{0}}.cshtml",
                    $"{club}/Themes/{theme}/Shared/{{0}}.cshtml",
                }
                .Concat(viewLocations).ToArray();
            }


            return viewLocations;
        }

        #endregion
    }
}
