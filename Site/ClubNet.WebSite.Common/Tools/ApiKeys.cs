namespace ClubNet.WebSite.Common.Tools
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a api/key couple
    /// </summary>
    [DataContract]
    public sealed class ApiKeys
    {
        #region Properties

        /// <summary>
        /// Gets or sets the public key
        /// </summary>
        [DataMember]
        public string Public { get; set; }

        /// <summary>
        /// Gets or sets the private key
        /// </summary>
        [DataMember]
        public string Private { get; set; }

        #endregion
    }
}
