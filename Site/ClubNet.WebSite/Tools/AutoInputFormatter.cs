namespace ClubNet.WebSite.Tools
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// Automatically try to map input with excpected result on the controller action
    /// </summary>
    public sealed class AutoInputFormatter : InputFormatter
    {
        #region Fields

        private const string FORM_CONTENT_TYPE = "application/x-www-form-urlencoded";
        private const string JSON_CONTENT_TYPE = "application/json";

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="AutoInputFormatter"/>
        /// </summary>
        public AutoInputFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(FORM_CONTENT_TYPE));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(JSON_CONTENT_TYPE));
        }

        #endregion

        #region Methods


        /// <summary>
        /// Allow application/x-www-form-urlencoded, application/json and no content type to
        /// be processed
        /// </summary>
        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) || contentType == FORM_CONTENT_TYPE || contentType == JSON_CONTENT_TYPE)
                return true;

            return false;
        }

        /// <summary>
        /// Handle text/plain or no content type for string results
        /// Handle application/octet-stream for byte[] results
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;
            string bodyContent = string.Empty;

            if (contentType == JSON_CONTENT_TYPE)
            {
                using (var reader = new StreamReader(request.Body))
                {
                    bodyContent = await reader.ReadToEndAsync();
                }
            }

            if (contentType == FORM_CONTENT_TYPE)
            {
                var formResult = await request.ReadFormAsync();

                if (context.ModelType.IsScalar())
                {
                    var key = formResult.Keys.FirstOrDefault(k => string.Equals(k.Replace("-", ""), context.Metadata.Name, StringComparison.OrdinalIgnoreCase));
                    if (!string.IsNullOrEmpty(key) && formResult.TryGetValue(key, out var values))
                        return await InputFormatterResult.SuccessAsync(Convert.ChangeType(values.FirstOrDefault(), context.ModelType));

                    return await InputFormatterResult.SuccessAsync(null);
                }

                bodyContent = ConvertToJson(formResult).ToString();
            }

            if (contentType == FORM_CONTENT_TYPE || contentType == JSON_CONTENT_TYPE)
            {
                if (!string.IsNullOrEmpty(bodyContent))
                {
                    var obj = JsonConvert.DeserializeObject(bodyContent, context.ModelType);
                    return await InputFormatterResult.SuccessAsync(obj);
                }
                return await InputFormatterResult.SuccessAsync(null);
            }
            return await InputFormatterResult.FailureAsync();
        }

        /// <summary>
        /// Convert the key value pair into json string
        /// </summary>
        private JObject ConvertToJson(IFormCollection formValues, string previousPath = null)
        {
            var obj = new JObject();

            var propKeys = formValues.Keys;
            var subModelKeys = propKeys.Where(k => k.Contains('.'))
                                       .ToArray();
            if (!string.IsNullOrEmpty(previousPath))
            {
                propKeys = formValues.Keys.Where(k => k.StartsWith(previousPath))
                                          .ToArray();

                subModelKeys = propKeys.Where(k => k.Substring(previousPath.Length)
                                                    .Trim('.')
                                                    .Contains('.'))
                                       .ToArray();

            }

            if (subModelKeys.Any())
            {
                subModelKeys = subModelKeys.Select(k => k.Split(".".ToArray(), 2, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault())
                                           .Where(k => k != null)
                                           .Distinct()
                                           .ToArray();
            }

            foreach (var subKey in subModelKeys)
            {
                var subOject = ConvertToJson(formValues, (string.IsNullOrEmpty(previousPath)
                                                                    ? subKey
                                                                    : previousPath + "." + subKey));

                obj.Add(subKey, subOject);
            }

            foreach (var key in propKeys.Except(subModelKeys))
            {
                var values = formValues[key];

                var propName = key.Replace("[", "")
                                  .Replace("]", "");

                if (!string.IsNullOrEmpty(previousPath))
                    propName = propName.Replace(previousPath, "")
                                       .Trim('.')
                                       .Trim();

                var isArray = key.EndsWith("[]");

                if (values.Count == 1 && !isArray)
                {
                    var val = values.First();
                    if (val.ToLower() == "on")
                        val = "true";
                    obj.Add(propName, val);
                }
                else if (isArray)
                {
                    obj.Add(propName, new JArray(values));
                }
            }

            return obj;
        }

        #endregion

    }
}
