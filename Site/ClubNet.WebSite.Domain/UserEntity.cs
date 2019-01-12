namespace ClubNet.WebSite.Domain
{
    using System;
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
        /// Gets the unique user id
        /// </summary>
        public Guid UserId { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Create(IUserInfo userInfo, SecurityCriteria securityCriteria)
        {
            base.Create(securityCriteria);
            UserId = userInfo.Id;
        }

        #endregion
    }
}
