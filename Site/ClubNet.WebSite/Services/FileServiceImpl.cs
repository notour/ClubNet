namespace ClubNet.WebSite.Services
{
    using System;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Define a service used to access file services
    /// </summary>
    internal sealed class FileServiceImpl : IFileService
    {
        #region Fields

        private const string MISSING_IMAGE_PATH = "~/content/missing_img.png";

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IClubDescriptor _clubDescriptor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="FileServiceImpl"/>
        /// </summary>
        public FileServiceImpl(IHttpContextAccessor contextAccessor, IClubDescriptor clubDescriptor)
        {
            this._contextAccessor = contextAccessor;
            this._clubDescriptor = clubDescriptor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the html path of the resource
        /// </summary>
        public string GetHtmlPath(SiteResources siteResource, TargetPlateform targetPlateform = TargetPlateform.None)
        {
            string path = string.Empty;
            if (this._clubDescriptor != null)
                path = this._clubDescriptor.GetSiteResource(siteResource, targetPlateform);

            if (!string.IsNullOrEmpty(path))
                return path;
            return MISSING_IMAGE_PATH;
        }

        public string GetHtmlPath(string key, TargetPlateform targetPlateform = TargetPlateform.None)
        {
            return MISSING_IMAGE_PATH;
        }

        public string GetHtmlPath(Guid id, TargetPlateform targetPlateform = TargetPlateform.None)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
