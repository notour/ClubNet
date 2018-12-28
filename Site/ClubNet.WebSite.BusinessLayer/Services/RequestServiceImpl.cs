namespace ClubNet.WebSite.BusinessLayer.Services
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;

    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Service that expose the request informations
    /// </summary>
    sealed class RequestServiceImpl : IRequestService
    {
        #region Fields

        private readonly IConfigService _defaultConfig;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="RequestServiceImpl"/>
        /// </summary>
        public RequestServiceImpl(HttpContext context)
        {
            _defaultConfig = context.RequestServices.GetService(typeof(IConfigService)) as IConfigService;

            var featureCulture = context.Features.Get<IRequestCultureFeature>();
            CurrentLanguage = featureCulture.RequestCulture.Culture;

            RequestId = Activity.Current?.Id ?? context.TraceIdentifier;

            CancellationToken = new CancellationTokenSource(_defaultConfig.DefaultRequestTimeout).Token;
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

        #endregion
    }
}
