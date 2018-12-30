namespace ClubNet.WebSite.Tools
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;
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

        private const string FormContentType = "application/x-www-form-urlencoded";
        private const string JsonContentType = "application/json";

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="AutoInputFormatter"/>
        /// </summary>
        public AutoInputFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(FormContentType));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(JsonContentType));
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
            if (string.IsNullOrEmpty(contentType) || contentType == FormContentType || contentType == JsonContentType)
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

            if (contentType == JsonContentType)
            {
                using (var reader = new StreamReader(request.Body))
                {
                    bodyContent = await reader.ReadToEndAsync();
                }
            }

            if (contentType == FormContentType)
            {
                var formResult = await request.ReadFormAsync();
                bodyContent = ConvertToJson(formResult);
            }

            if (contentType == FormContentType || contentType == JsonContentType)
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
        private string ConvertToJson(IFormCollection formValues)
        {
            return "{" + string.Join(", ", formValues.Keys.Cast<string>().Select(k => "\"" + k + "\":\"" + formValues[k].FirstOrDefault() + "\"")) + "}";
        }

        #endregion

    }
}
