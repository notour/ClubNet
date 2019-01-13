namespace ClubNet.WebSite.Domain.Sport
{
    using System;

    /// <summary>
    /// Base class of the saison entities
    /// </summary>
    public interface ISeasonEntity<TEntityType> : IEntity<TEntityType>
    {
        #region Properties

        /// <summary>
        /// Gets the season unique id
        /// </summary>
        Guid SeasonId { get; }

        #endregion
    }
}
