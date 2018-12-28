namespace ClubNet.WebSite.Controllers
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Middleware;
    using ClubNet.WebSite.NusinessLayer.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Base custom class of all controllers
    /// </summary>
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public abstract class BaseController : Controller
    {
        #region Fields

        private readonly IHttpContextAccessor _contextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="BaseController"/>
        /// </summary>
        public BaseController(IServiceProvider serviceProvider)
        {
            _contextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets the current request Service
        /// </summary>
        protected IRequestService RequestService
        {
            get { return _contextAccessor.CurrentRequestService(); }
        }

        #endregion
    }
}
