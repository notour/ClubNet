namespace ClubNet.WebSite.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Define a base type for all the entity savabled
    /// </summary>
    public interface IUserEntity : IEntity
    {
        #region Properties

        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        IEnumerable<Guid> Owners { get; }

        #endregion
    }

    /// <summary>
    /// Define a base type for all the entity savabled
    /// </summary>
    public interface IUserEntity<TEntityType> : IUserEntity, IEntity<TEntityType>
    {
    }
}
