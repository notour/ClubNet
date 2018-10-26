using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ClubNet.WebSite.Common.Errors;

namespace ClubNet.WebSite.Domain.Logs
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public sealed class ErrorLog : LogBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the error category
        /// </summary>
        public ErrorCategory ErrorCategory { get; set; } 

        /// <summary>
        /// Gets or sets the error internal code
        /// </summary>
        public InternalErrorCodes ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the context key used to add more precision in the error
        /// </summary>
        public string ContextKey { get; set; }

        /// <summary>
        /// Gets or sets the context arguments
        /// </summary>
        public IEnumerable<string> Contexts { get; set; }

        #endregion
    }
}
