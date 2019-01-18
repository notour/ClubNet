namespace ClubNet.Shared.Api.Dto
{
    using System;

    /// <summary>
    /// base dto of the subscription form
    /// </summary>
    public interface INewSubscriptionBaseDto
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating the subscription season id
        /// </summary>
        Guid SeasonId { get; }

        /// <summary>
        /// Gets the member first name
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Gets the member last name
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// Gets the member birth date
        /// </summary>
        DateTime? BirthDate { get; }

        #endregion
    }
}
