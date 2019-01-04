namespace ClubNet.Phoenix
{
    using System.IO;
    using System.Reflection;

    using ClubNet.Phoenix.Configuration;
    using ClubNet.WebSite.Common.Configurations;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Common.Tools;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.FileProviders;

    using Newtonsoft.Json;

    /// <summary>
    /// Describe all the hard and default data associate to the specific club
    /// </summary>
    public class ClubDescriptor : IClubDescriptor
    {
        private const string CONFIG_FILE = "Config.json";

        private readonly PhoenixConfig _config;
        private readonly string _absoluteRootPath;
        private readonly string _rootPath;

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ClubDescriptor"/>
        /// </summary>
        public ClubDescriptor()
        {
            this._absoluteRootPath = Path.GetDirectoryName(typeof(ClubDescriptor).Assembly.Location);
            this._rootPath = "/" + Path.GetRelativePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), this._absoluteRootPath).Replace("\\", "/");
            using (var configStream = new StreamReader(File.OpenRead(Path.Combine(this._absoluteRootPath, CONFIG_FILE))))
            {
                this._config = JsonConvert.DeserializeObject<PhoenixConfig>(configStream.ReadToEnd());

                if (this._config != null && this._config.ApiKeys != null)
                    this.ClubApiKeyProvider = new ApiKeyProvider(this._config.ApiKeys);
            }

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the phoenix display name
        /// </summary>
        public string DisplayName
        {
            get { return this._config.DisplayName; }
        }

        /// <summary>
        /// Gets the club api keys
        /// </summary>
        public IApiKeyProvider ClubApiKeyProvider { get; }

        /// <summary>
        /// Gets the email settings
        /// </summary>
        public EmailSettings EmailSettings { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Configure the specific 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(this._absoluteRootPath, "Content")),
                RequestPath = _rootPath
            });
        }

        /// <summary>
        /// Gets the resource 
        /// </summary>
        public string GetSiteResource(SiteResources siteResource, TargetPlateform targetPlateform)
        {
            return this._config.Media[siteResource].Replace("~", this._rootPath);
        }

        #endregion
    }
}
