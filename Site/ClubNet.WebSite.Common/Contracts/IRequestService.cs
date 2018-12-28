namespace ClubNet.WebSite.BusinessLayer.Contracts
{
    using ClubNet.WebSite.Common.Enums;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Define a current service that expose the request informations
    /// </summary>
    public interface IRequestService
    {
        #region Properties

        /// <summary>
        /// Gets the current query language
        /// </summary>
        CultureInfo CurrentLanguage { get; }

        /// <summary>
        /// Gets the current request id
        /// </summary>
        string RequestId { get; }

        /// <summary>
        /// Gets the request cancellation token
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Gets the current request platform
        /// </summary>
        TargetPlateform RequestPlatform { get; }

        #endregion
    }
}
