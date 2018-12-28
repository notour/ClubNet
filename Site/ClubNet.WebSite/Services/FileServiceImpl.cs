namespace ClubNet.WebSite.Services
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;

    using Microsoft.AspNetCore.Http;

    using System;

    /// <summary>
    /// Define a service used to access file services
    /// </summary>
    sealed class FileServiceImpl : IFileService
    {
        #region Fields

        private const string MissingImagePath = "~/content/missing_img.png";

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IClubDescriptor _clubDescriptor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="FileServiceImpl"/>
        /// </summary>
        public FileServiceImpl(IHttpContextAccessor contextAccessor, IClubDescriptor clubDescriptor)
        {
            _contextAccessor = contextAccessor;
            _clubDescriptor = clubDescriptor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the html path of the resource
        /// </summary>
        public string GetHtmlPath(SiteResources siteResource, TargetPlateform targetPlateform = TargetPlateform.None)
        {
            string path = string.Empty;
            if (_clubDescriptor != null)
                path = _clubDescriptor.GetSiteResource(siteResource, targetPlateform);

            if (!string.IsNullOrEmpty(path))
                return path;
            return MissingImagePath;
        }

        public string GetHtmlPath(string key, TargetPlateform targetPlateform = TargetPlateform.None)
        {
            return MissingImagePath;
        }

        public string GetHtmlPath(Guid id, TargetPlateform targetPlateform = TargetPlateform.None)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
