namespace ClubNet.WebSite.Tools
{
    using Newtonsoft.Json.Linq;

    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Define the recaptcha tools 
    /// </summary>
    public sealed class ReCaptcha
    {
        #region Methods

        // Info : https://dejanstojanovic.net/aspnet/2018/may/using-google-recaptcha-v2-in-aspnet-core-web-application/

        /// <summary>
        /// Validate the recaptcha
        /// </summary>
        public static async Task<bool> ValidateAsync(string reCaptchaSecret, IDictionary<string, string> httpRequestHeader)
        {
            string reCaptchResponse = string.Empty;
            if (!httpRequestHeader.TryGetValue("g-recaptcha-response", out reCaptchResponse))
                return false;

            using (HttpClient httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaSecret}&response={reCaptchResponse}");
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    return false;

                String jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
                dynamic jsonData = JObject.Parse(jsonResponse);
                if (jsonData.success != true.ToString().ToLower())
                    return false;

                return true;
            }
        }

        #endregion
    }
}
