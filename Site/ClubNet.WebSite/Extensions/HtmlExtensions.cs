namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;

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
        /// Custom implementation of the script section to allowed partial usage
        /// </summary>
        public static HtmlString PartialScript(this IHtmlHelper htmlHelper, string partialScriptName)
        {
            htmlHelper.ViewContext.HttpContext.Items["_partial_script_" + Guid.NewGuid()] = partialScriptName;
            return HtmlString.Empty;
        }

        /// <summary>
        /// Render custom section script
        /// </summary>
        public static async Task<HtmlString> RenderScriptsAsync(this IHtmlHelper htmlHelper)
        {
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys.OfType<string>().Where(k => k.StartsWith("_script_")))
            {
                var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                if (template != null)
                {
                    htmlHelper.ViewContext.Writer.Write(template(null));
                }
            }

            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys.OfType<string>().Where(k => k.StartsWith("_partial_script_")))
            {
                var partial = htmlHelper.ViewContext.HttpContext.Items[key] as string;
                if (partial != null)
                {
                    htmlHelper.ViewContext.Writer.Write(await htmlHelper.PartialAsync(partial));
                }
            }
            return HtmlString.Empty;
        }
    }
}
