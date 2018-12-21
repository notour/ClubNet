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

namespace ClubNet.WebSite.BusinessLayer.Logic
{
    class ThemeBL : IThemeBL
    {
        #region Fields

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfigService _configService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ThemeBL"/>
        /// </summary>
        public ThemeBL(IHttpContextAccessor contextAccessor, IConfigService configService)
        {
            _contextAccessor = contextAccessor;
            _configService = configService;
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
                if (_contextAccessor != null && _contextAccessor.HttpContext != null)
                {

                }
                return _configService.DefaultTheme;
            }
        }

        #endregion
    }
}
