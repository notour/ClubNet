namespace ClubNet.WebSite.Domain.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a security right
    /// </summary>
    [DataContract]
    public sealed class SecurityRightGroup : SecurityEntity<SecurityEntityType>
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityRightGroup"/> class.
        /// </summary>
        public SecurityRightGroup(Guid id, string name, string descriptions, IEnumerable<SecurityRight> rights)
            : base(id, name, descriptions, SecurityEntityType.RightGroup)
        {
            this.Rights = rights;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the rights.
        /// </summary>
        [DataMember]
        public IEnumerable<SecurityRight> Rights { get; }

        #endregion
    }
}
