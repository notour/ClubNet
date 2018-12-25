namespace ClubNet.WebSite.Tools
{
    using ClubNet.WebSite.BusinessLayer.Contracts;

    using Microsoft.AspNetCore.Mvc.Razor;

    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Expand the view location to clud the theme systems
    /// </summary>
    class TenantViewLocationExpander : IViewLocationExpander
    {
        #region Fields

        private const string THEME_KEY = "theme";

        #endregion

        #region Methods

        /// <summary>
        /// Add the theme key into the http context
        /// </summary>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[THEME_KEY] = ((IThemeBL)context.ActionContext.HttpContext.RequestServices.GetService(typeof(IThemeBL)))?.CurrentThemeName;
        }

        /// <summary>
        /// Append view location resolver path
        /// </summary>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string theme = null;
            if (context.Values.TryGetValue(THEME_KEY, out theme) && !string.IsNullOrEmpty(theme))
            {
                viewLocations = new[] {
                    $"/Themes/{theme}/{{1}}/{{0}}.cshtml",
                    $"/Themes/{theme}/Shared/{{0}}.cshtml",
                }
                .Concat(viewLocations).ToArray();
            }


            return viewLocations;
        }

        #endregion
    }
}
