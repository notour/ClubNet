namespace ClubNet.WebSite.Domain
{
    using System;
    using System.Runtime.Serialization;

    using ClubNet.WebSite.Domain.Security;

    using MongoDB.Bson.Serialization.Attributes;

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
        public TEntityType EntityType { get; private set; }

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
        [BsonElement]
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// Gets the created entity ddate
        /// </summary>
        [BsonElement]
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets the latest update entity ddate
        /// </summary>
        [BsonElement]
        public DateTime UpdatedOn { get; private set; }

        /// <summary>
        /// Gets a value indicating if this document is a draft
        /// </summary>
        [BsonElement]
        public bool IsDraft { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Create(SecurityCriteria securityCriteria, bool isDraft)
        {
            SetData(Guid.NewGuid(), securityCriteria, isDraft);
            CreatedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Update(SecurityCriteria securityCriteria, bool isDraft)
        {
            SetData(this.Id, securityCriteria, isDraft);
        }

        /// <summary>
        /// Initialize the entity data
        /// </summary>
        private void SetData(Guid id, SecurityCriteria securityCriteria, bool isDraft)
        {
            Id = id;
            SecurityCriteria = securityCriteria;
            UpdatedOn = DateTime.UtcNow;
            IsDraft = isDraft;
        }

        #endregion
    }
}
