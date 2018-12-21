using ClubNet.WebSite.Domain.Security;

using System;

namespace ClubNet.WebSite.Domain
{
    /// <summary>
    /// Define an entity with security criteria
    /// </summary>
    public interface ISecurityEntity
    {
        #region Properties

        /// <summary>
        /// Gets the entity Id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the security criteria
        /// </summary>
        SecurityCriteria SecurityCriteria { get; }

        #endregion
    }
}
