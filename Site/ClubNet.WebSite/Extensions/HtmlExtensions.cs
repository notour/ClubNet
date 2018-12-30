namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Linq;

    public static class HtmlExtensions
    {
        /// <summary>
        /// Custom implementation of the script section to allowed partial usage
        /// </summary>
        public static HtmlString Script(this IHtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
            return HtmlString.Empty;
        }

        /// <summary>
        /// Render custom section script
        /// </summary>
        public static HtmlString RenderScripts(this IHtmlHelper htmlHelper)
        {
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys.OfType<string>().Where(k => k.StartsWith("_script_")))
            {
                var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                if (template != null)
                {
                    htmlHelper.ViewContext.Writer.Write(template(null));
                }
            }
            return HtmlString.Empty;
        }
    }
}
