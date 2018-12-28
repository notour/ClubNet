namespace ClubNet.WebSite.Common.Contracts
{
    using ClubNet.WebSite.Common.Errors;

    using System;

    /// <summary>
    /// Define a service able to managed all the kind of error in the system
    /// </summary>
    public interface IErrorService
    {
        /// <summary>
        /// Log an error to keep a trace
        /// </summary>
        Guid LogError(ErrorCategory errorCategory, InternalErrorCodes errorCode, string contextKey, params string[] context);

        /// <summary>
        /// Log an error to keep a trace
        /// </summary>
        Guid LogError(ErrorCategory errorCategory, InternalErrorCodes errorCode, string contextKey, Exception ex, params string[] context);

        /// <summary>
        /// Provide the error description
        /// </summary>
        string GetErrorDescription(ErrorCategory errorCategory, InternalErrorCodes errorCode, string contextKey, params string[] context);

        /// <summary>
        /// Provide the error description using the errorId
        /// </summary>
        string GetErrorDescription(ErrorCategory errorCategory, InternalErrorCodes errorCode, Guid errorLoggedId);
    }
}
