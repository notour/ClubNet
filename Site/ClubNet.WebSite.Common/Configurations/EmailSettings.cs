using ClubNet.Framework.Attributes;

namespace ClubNet.WebSite.Common.Configurations
{
    /// <summary>
    /// Define all the email sender configurations
    /// </summary>
    [ConfigurationData(ConfigurationSectionKey)]
    public class EmailSettings
    {
        #region Fields

        public const string ConfigurationSectionKey = nameof(EmailSettings);

        #endregion

        #region Properties

        public string Domain { get; set; }

        public int Port { get; set; }

        public string SecondayDomain { get; set; }

        public int SecondaryPort { get; set; }

        public string UsernameEmail { get; set; }

        public string UsernamePassword { get; set; }

        public string DefaultFromEmail { get; set; }

        public string AdminEmail { get; set; }

        #endregion
    }
}
