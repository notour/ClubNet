namespace ClubNet.WebSite.Common.Contracts
{
    using System.Globalization;

    /// <summary>
    /// Service in charge to provide email template
    /// </summary>
    public interface IEmailTemplateProvider
    {
        /// <summary>
        /// Get the html template of the email
        /// </summary>
        string GetTemplate(string key, CultureInfo culture);

        /// <summary>
        /// Gets the subject
        /// </summary>
        string GetSubject(string key, CultureInfo culture);
    }
}
