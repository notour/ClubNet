namespace ClubNet.WebSite.Domain
{
    using ClubNet.WebSite.Domain.Security;
    using MongoDB.Bson.Serialization.Attributes;
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
        /// Initialize a new instance of the class <see cref="Entity"/>
        /// </summary>
        protected Entity(TEntityType entityType)
        {
            EntityType = entityType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the right unique identifier.
        /// </summary>
        [BsonId]
        [DataMember(IsRequired = true)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [DataMember(IsRequired = true)]
        public TEntityType EntityType { get; }

        /// <summary>
        /// Gets the entity security criteria
        /// </summary>
        [BsonElement]
        [BsonIgnoreIfDefault]
        [DataMember(EmitDefaultValue = false)]
        public SecurityCriteria SecurityCriteria { get; private set; }

        /// <summary>
        /// Gets or sets the field used to prevent cross save
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Create(SecurityCriteria securityCriteria)
        {
            SetData(Guid.NewGuid(), securityCriteria);
        }

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Update(SecurityCriteria securityCriteria)
        {
            SetData(this.Id, securityCriteria);
        }

        /// <summary>
        /// Initialize the entity data
        /// </summary>
        private void SetData(Guid id, SecurityCriteria securityCriteria)
        {
            Id = id;
            SecurityCriteria = securityCriteria;
        }

        #endregion
    }
}
