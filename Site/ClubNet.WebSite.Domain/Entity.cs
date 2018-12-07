using System;
using System.Runtime.Serialization;

namespace ClubNet.WebSite.Domain
{
    /// <summary>
    /// Define an entity savable
    /// </summary>
    [DataContract]
    public abstract class Entity<TEntityType> : IEntity<TEntityType>
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TEntityType}"/> class.
        /// </summary>
        protected Entity(Guid id, TEntityType entityType)
        {
            Id = id;
            EntityType = entityType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the right unique identifier.
        /// </summary>
        [DataMember]
        public Guid Id { get; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        [DataMember]
        public TEntityType EntityType { get; }

        #endregion
    }
}
