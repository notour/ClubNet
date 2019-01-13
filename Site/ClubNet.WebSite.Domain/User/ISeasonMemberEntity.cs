namespace ClubNet.WebSite.Domain.Sport
{
    using System;

    /// <summary>
    /// Base class of the saison entities
    /// </summary>
    public interface ISeasonMemberEntity<TEntityType> : IEntity<TEntityType>, ISeasonEntity<TEntityType>
    {
        #region Properties

        /// <summary>
        /// Gets the current member id
        /// </summary>
        Guid MemberId { get; }

        #endregion
    }
}
