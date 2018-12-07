using System;

namespace ClubNet.WebSite.Domain
{
    /// <summary>
    /// Define a base type for all the entity savabled
    /// </summary>
    public interface IEntity<TEntityType>
    {
        #region Properties

        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        TEntityType EntityType { get; }

        #endregion
    }
}
