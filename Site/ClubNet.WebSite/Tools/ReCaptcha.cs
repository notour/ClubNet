namespace ClubNet.WebSite.Tools
{
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json.Linq;

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
        public static async Task<bool> ValidateAsync(string reCaptchaSecret, string reCaptchResponse)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaSecret}&response={reCaptchResponse}");
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    return false;

                var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                dynamic jsonData = JObject.Parse(jsonResponse);
                if (jsonData.success != true.ToString().ToLower())
                    return false;

                return true;
            }
        }

        #endregion
    }
}
