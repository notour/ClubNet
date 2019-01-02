namespace ClubNet.WebSite.ViewModels
{
    using ClubNet.WebSite.Common.Contracts;

    /// <summary>
    /// View model of the error page
    /// </summary>
    public sealed class ErrorViewModel
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ErrorViewModel"/>
        /// </summary>
        public ErrorViewModel(IRequestService requestService) 
        {
            RequestId = requestService.RequestId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the request id
        /// </summary>
        public string RequestId { get; }

        #endregion
    }
}
