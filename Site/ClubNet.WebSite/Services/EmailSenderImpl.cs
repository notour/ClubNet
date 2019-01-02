namespace ClubNet.WebSite.Services
{
    using ClubNet.WebSite.Common.Configurations;
    using ClubNet.WebSite.Common.Contracts;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    /// <summary>
    /// Aspnet core implementation of the Email sender
    /// </summary>
    class EmailSenderImpl : IEmailSender
    {
        #region Fields

        private readonly IClubDescriptor _clubDescriptor;
        private readonly ILogger<IEmailSender> _logger;
        private readonly EmailSettings _settings;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="EmailSenderImpl"/>
        /// </summary>
        public EmailSenderImpl(IOptions<EmailSettings> settings, IClubDescriptor clubDescriptor, ILogger<IEmailSender> logger)
        {
            this._settings = settings.Value;

            if (clubDescriptor != null && clubDescriptor.EmailSettings != null)
                this._settings = clubDescriptor.EmailSettings;

            this._clubDescriptor = clubDescriptor;
            this._logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Send an email
        /// </summary>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var toEmail = email;
                if (string.IsNullOrEmpty(email))
                    toEmail = this._settings.AdminEmail;

                var mail = new MailMessage()
                {
                    From = new MailAddress(this._settings.UsernameEmail)
                };
                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = subject;
                mail.Body = htmlMessage;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (var smtp = new SmtpClient(this._settings.Domain, this._settings.Port))
                {
                    smtp.Credentials = new NetworkCredential(this._settings.UsernameEmail, this._settings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                //do something here
                this._logger.LogError(ex, subject + ":" + htmlMessage);
            }
        }

    }

    #endregion
}
