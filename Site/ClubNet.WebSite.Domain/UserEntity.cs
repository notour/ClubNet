namespace ClubNet.WebSite.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using ClubNet.WebSite.Common;
    using ClubNet.WebSite.Domain.Security;

    /// <summary>
    /// Define an entity savable
    /// </summary>
    [DataContract]
    public abstract class UserEntity<TEntityType> : Entity<TEntityType>, ISecurityEntity
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserEntity"/>
        /// </summary>
        protected UserEntity(TEntityType entityType)
            : base(entityType)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique user id of the owner ids
        /// </summary>
        public IEnumerable<Guid> Owners { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Create(IEnumerable<IUserInfo> owners, SecurityCriteria securityCriteria)
        {
            base.Create(securityCriteria);
            this.Owners = owners.Select(o => o.Id)
                                .ToArray();
        }

        #endregion
    }
}
