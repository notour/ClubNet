namespace ClubNet.Phoenix
{
    using ClubNet.Phoenix.Configuration;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.FileProviders;
    using Newtonsoft.Json;

    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Describe all the hard and default data associate to the specific club
    /// </summary>
    public class ClubDescriptor : IClubDescriptor
    {
        private const string ConfigFile = "Config.json";

        private readonly PhoenixConfig _config;
        private readonly string _absoluteRootPath;
        private readonly string _rootPath;

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ClubDescriptor"/>
        /// </summary>
        public ClubDescriptor()
        {
            _absoluteRootPath = Path.GetDirectoryName(typeof(ClubDescriptor).Assembly.Location);
            _rootPath = "/" + Path.GetRelativePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), _absoluteRootPath).Replace("\\", "/");
            using (var configStream = new StreamReader(File.OpenRead(Path.Combine(_absoluteRootPath, ConfigFile))))
            {
                this._config = JsonConvert.DeserializeObject<PhoenixConfig>(configStream.ReadToEnd());
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
                FileProvider = new PhysicalFileProvider(Path.Combine(_absoluteRootPath, "Content")),
                RequestPath = _rootPath
            });
        }

        /// <summary>
        /// Gets the resource 
        /// </summary>
        public string GetSiteResource(SiteResources siteResource, TargetPlateform targetPlateform)
        {
            return _config.Media[siteResource].Replace("~", _rootPath);
        }

        #endregion
    }
}
