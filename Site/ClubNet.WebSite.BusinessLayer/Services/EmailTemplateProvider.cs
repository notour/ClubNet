namespace ClubNet.WebSite.BusinessLayer.Services
{
    using ClubNet.WebSite.Common.Contracts;
    using Microsoft.Extensions.FileProviders;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Email template provider system
    /// </summary>
    public sealed class EmailTemplateProvider : IEmailTemplateProvider
    {
        #region Fields

        private readonly IEnumerable<string> _defaultEmbededEmailsResourceNames;
        private readonly IEnumerable<string> _clubDescriptorResourceNames;
        private readonly IClubDescriptor _clubDescriptor;
        private readonly ResourceManager _subjectResource;

        private IImmutableDictionary<string, string> _emailTemplates;

        private readonly Assembly _clubDescriptorAssembly;
        private readonly Assembly _defaultEmbededEmails;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a ne instance of the class <see cref="EmailTemplateProvider"/>
        /// </summary>
        public EmailTemplateProvider(Assembly defaultEmbededEmails, IClubDescriptor clubDescriptor, ResourceManager subjectResource)
        {
            this._defaultEmbededEmails = defaultEmbededEmails;
            this._defaultEmbededEmailsResourceNames = this._defaultEmbededEmails.GetManifestResourceNames();

            if (clubDescriptor != null)
            {
                this._clubDescriptorAssembly = clubDescriptor.GetType().Assembly;
                this._clubDescriptorResourceNames = this._clubDescriptorAssembly.GetManifestResourceNames();
                this._clubDescriptor = clubDescriptor;
            }

            this._subjectResource = subjectResource;
            this._emailTemplates = ImmutableDictionary<string, string>.Empty;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the email template
        /// </summary>
        public string GetTemplate(string key, CultureInfo culture)
        {
            string resourceKey = (key + "." + culture.TwoLetterISOLanguageName + ".cshtml").ToLowerInvariant();
            string template = string.Empty;

            if (!this._emailTemplates.TryGetValue(resourceKey, out template))
            {
                lock (this)
                {
                    if (!this._emailTemplates.TryGetValue(resourceKey, out template))
                    {
                        template = ExtractResource(resourceKey, this._clubDescriptorResourceNames, this._clubDescriptorAssembly);

                        if (string.IsNullOrEmpty(template))
                            template = ExtractResource(resourceKey, this._defaultEmbededEmailsResourceNames, this._defaultEmbededEmails);

                        this._emailTemplates = this._emailTemplates.Add(resourceKey, template);
                    }
                }
            }

            return template;
        }

        /// <summary>
        /// Gets the email subject
        /// </summary>
        public string GetSubject(string key, CultureInfo culture)
        {
            string club = "ClubNet";
            if (this._clubDescriptor != null)
                club = this._clubDescriptor.DisplayName;

            return $"[{club}] {this._subjectResource.GetString(key, culture)}";
        }

        /// <summary>
        /// Extract the template resource
        /// </summary>
        private string ExtractResource(string resourceKey, IEnumerable<string> resourceNames, Assembly assembly)
        {
            if (resourceNames != null)
            {
                var fullResourceKey = resourceNames.FirstOrDefault(n => n.EndsWith(resourceKey, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(fullResourceKey))
                {
                    using (var streamReader = new StreamReader(assembly.GetManifestResourceStream(fullResourceKey)))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
