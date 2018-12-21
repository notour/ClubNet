using ClubNet.WebSite.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubNet.WebSite.Tools
{
    /// <summary>
    /// 
    /// </summary>
    class TenantViewLocationExpander : IViewLocationExpander
    {
        private const string THEME_KEY = "theme";

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[THEME_KEY] = ((IThemeBL)context.ActionContext.HttpContext.RequestServices.GetService(typeof(IThemeBL)))?.CurrentThemeName;
        }

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
    }
}
