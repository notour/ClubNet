namespace ClubNet.WebSite.BusinessLayer.Logic
{
    using ClubNet.Framework.Memory;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;

    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Base class of the business layer classes
    /// </summary>
    internal abstract class BaseBL : Disposable
    {
        #region Fields

        private readonly IHttpContextAccessor _contextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ThemeBL"/>
        /// </summary>
        protected BaseBL(IHttpContextAccessor contextAccessor, ISecurityBL securityBL, IConfigService configService)
        {
            this._contextAccessor = contextAccessor;
            this.ConfigService = configService;
            this.SecurityBL = securityBL;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the configuration service
        /// </summary>
        protected IRequestService RequestService
        {
            get { return this._contextAccessor.CurrentRequestService(); }
        }

        /// <summary>
        /// Gets the current config service
        /// </summary>
        public IConfigService ConfigService { get; }

        /// <summary>
        /// Gets the business layer in charge of the security
        /// </summary>
        public ISecurityBL SecurityBL { get; }


        #endregion
    }
}
