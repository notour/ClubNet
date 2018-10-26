using System;
using System.Runtime.Serialization;

using ClubNet.WebSite.Common.Enums;

namespace ClubNet.WebSite.Domain.Logs
{
    /// <summary>
    /// Base class for all the log
    /// </summary>
    [DataContract]
    public abstract class LogBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique error log id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the log category
        /// </summary>
        [DataMember]
        public LogCategory Category { get; set; }

        /// <summary>
        /// Gets or sets an english displayable message
        /// </summary>
        [DataMember]
        public string DisplayMessage { get; set; }

        /// <summary>
        /// Gets or sets the utc date of the log
        /// </summary>
        [DataMember]
        public DateTime WhenUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicting where the log occured
        /// </summary>
        [DataMember]
        public string WhereUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the user that trigger the log
        /// </summary>
        [DataMember]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the context if the log
        /// </summary>
        [DataMember]
        public string RequestUrl { get; set; }

        #endregion
    }
}
