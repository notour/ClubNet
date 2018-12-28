namespace ClubNet.WebSite.BusinessLayer.Logic
{
    using ClubNet.WebSite.BusinessLayer.Configurations;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.DataLayer;
    using ClubNet.WebSite.Domain.Configs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;


    /// <summary>
    /// Business layer in charge of the theme managment
    /// </summary>
    class ThemeBL : BaseBL, IThemeBL
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ThemeBL"/>
        /// </summary>
        public ThemeBL(IHttpContextAccessor contextAccessor, IConfigService configService, ISecurityBL securityBL)
            : base(contextAccessor, securityBL, configService)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current theme based on the http context
        /// </summary>
        public string CurrentThemeName
        {
            get
            {
                //if (ContextAccessor != null && ContextAccessor.HttpContext != null)
                //{

                //}
                //return RequestService.DefaultTheme;
                return ConfigService.DefaultTheme;
            }
        }

        #endregion
    }
}
