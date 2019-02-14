namespace ClubNet.WebSite.Common.Services
{
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;

    /// <summary>
    /// Service that expose the request informations
    /// </summary>
    sealed class RequestServiceImpl : IRequestService
    {
        #region Fields

        private readonly IConfigService _defaultConfig;
        private readonly HttpContext _context;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="RequestServiceImpl"/>
        /// </summary>
        public RequestServiceImpl(HttpContext context)
        {
            this._defaultConfig = context.RequestServices.GetService(typeof(IConfigService)) as IConfigService;
            this._context = context;

            var featureCulture = context.Features.Get<IRequestCultureFeature>();
            CurrentLanguage = featureCulture.RequestCulture.Culture;

            RequestId = Activity.Current?.Id ?? context.TraceIdentifier;
            if (context.User.Identity.IsAuthenticated)
            {
                var userIdStr = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdStr != null && Guid.TryParse(userIdStr, out var userId))
                    UserId = userId;
            }
            CancellationToken = new CancellationTokenSource(Debugger.IsAttached ? TimeSpan.FromSeconds(30) : this._defaultConfig.DefaultRequestTimeout).Token;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the request current languages
        /// </summary>
        public CultureInfo CurrentLanguage { get; }

        /// <summary>
        /// Gets the request id
        /// </summary>
        public string RequestId { get; }

        /// <summary>
        /// Gets the request cancellation token
        /// </summary>
        public CancellationToken CancellationToken { get; }

        public TargetPlateform RequestPlatform
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the current connected user id
        /// </summary>
        public Guid UserId { get; }

        #endregion
    }
}
