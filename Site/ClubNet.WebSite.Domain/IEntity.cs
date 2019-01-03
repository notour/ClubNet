namespace ClubNet.WebSite.Domain
{
    using System;
    
    /// <summary>
    /// Define a base type for all the entity savabled
    /// </summary>
    public interface IEntity
    {
        #region Properties

        /// <summary>
        /// Gets the entity identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the concurrency stamp to prevent concurrency changes
        /// </summary>
        string ConcurrencyStamp { get; set; }

        #endregion
    }

    /// <summary>
    /// Define a base type for all the entity savabled
    /// </summary>
    public interface IEntity<TEntityType> : IEntity
    {
        #region Properties


        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        TEntityType EntityType { get; }

        #endregion
    }
}
