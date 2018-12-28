﻿namespace ClubNet.WebSite.Common.Contracts
{
    using ClubNet.WebSite.Common.Enums;
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Define a specific club descriptor used to customize the club net environment
    /// </summary>
    public interface IClubDescriptor
    {
        #region Methods

        /// <summary>
        /// Configure the club plugin
        /// </summary>
        void Configure(IApplicationBuilder app);

        /// <summary>
        /// Gets the resource path if exists
        /// </summary>
        string GetSiteResource(SiteResources siteResource, TargetPlateform targetPlateform);

        #endregion
    }
}