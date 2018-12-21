namespace ClubNet.WebSite.Domain
{
    using ClubNet.WebSite.Domain.Security;
    using System;

    using System.Runtime.Serialization;

    /// <summary>
    /// Define an entity savable
    /// </summary>
    [DataContract]
    public abstract class Entity<TEntityType> : IEntity<TEntityType>, ISecurityEntity
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TEntityType}"/> class.
        /// </summary>
        protected Entity(Guid id, TEntityType entityType, SecurityCriteria securityCriteria = null)
        {
            Id = id;
            EntityType = entityType;
            SecurityCriteria = securityCriteria;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the right unique identifier.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid Id { get; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        [DataMember(IsRequired = true)]
        public TEntityType EntityType { get; }

        /// <summary>
        /// Gets the entity security criteria
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public SecurityCriteria SecurityCriteria { get; }

        #endregion
    }
}
