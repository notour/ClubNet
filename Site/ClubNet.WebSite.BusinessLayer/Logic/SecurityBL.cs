﻿namespace ClubNet.WebSite.BusinessLayer.Logic
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Domain;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Define the business layer available to managed the security
    /// </summary>
    class SecurityBL : ISecurityBL
    {
        #region Methods

        /// <summary>
        /// Filter the allowed entities in function of the security user
        /// </summary>
        public Task<IEnumerable<ISecurityEntity>> FilterEntityAsync(IEnumerable<ISecurityEntity> entities, HttpContext httpContext)
        {
            return Task<IEnumerable<ISecurityEntity>>.FromResult(entities);
        }

        #endregion
    }
}