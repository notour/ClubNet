using ClubNet.Framework.Memory;
using ClubNet.WebSite.BusinessLayer.Contracts;

using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading;

namespace ClubNet.WebSite.BusinessLayer.Logic
{
    /// <summary>
    /// Base class of the business layer classes
    /// </summary>
    abstract class BaseBL : Disposable
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ThemeBL"/>
        /// </summary>
        protected BaseBL(IHttpContextAccessor contextAccessor, IConfigService configService)
        {
           ContextAccessor = contextAccessor;
           ConfigService = configService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the accessor to the HttpContext
        /// </summary>
        protected IHttpContextAccessor ContextAccessor { get; }

        /// <summary>
        /// Gets the configuration service
        /// </summary>
        protected IConfigService ConfigService { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the cancellation timeout
        /// </summary>
        protected CancellationTokenSource GetTimeoutToken()
        {
#if DEBUG
            //if (Debugger.IsAttached)
            //    return new CancellationTokenSource();
#endif
            return new CancellationTokenSource(ConfigService.DefaultTimeout);
        }

        #endregion
    }
}
