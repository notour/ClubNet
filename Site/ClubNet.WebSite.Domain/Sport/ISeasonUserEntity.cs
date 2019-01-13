namespace ClubNet.WebSite.Domain.Sport
{
    using System;

    /// <summary>
    /// Base class of the saison entities
    /// </summary>
    public interface ISeasonUserEntity<TEntityType> : IUserEntity<TEntityType>, ISeasonEntity<TEntityType>
    {
    }
}
