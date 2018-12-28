namespace ClubNet.WebSite.Common.Contracts
{
    using ClubNet.WebSite.Common.Enums;

    using System;

    /// <summary>
    /// Define a file service in charge of handling all the path and access to the file system
    /// </summary>
    public interface IFileService
    {
        #region Methods

        /// <summary>
        /// Gets the public html path to access a specific file resource
        /// </summary>
        string GetHtmlPath(SiteResources siteResource, TargetPlateform targetPlateform = TargetPlateform.None);

        /// <summary>
        /// Gets the public html path to access a specific file resource
        /// </summary>
        string GetHtmlPath(string key, TargetPlateform targetPlateform = TargetPlateform.None);

        /// <summary>
        /// Gets the public html path to access a specific file resource
        /// </summary>
        string GetHtmlPath(Guid id, TargetPlateform targetPlateform = TargetPlateform.None);

        #endregion
    }
}
