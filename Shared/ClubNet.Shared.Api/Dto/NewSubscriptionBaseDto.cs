namespace ClubNet.Shared.Api.Dto
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class NewSubscriptionBaseDto : INewSubscriptionBaseDto
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating the subscription season id
        /// </summary>
        [DataMember]
        public Guid SeasonId { get; set; }

        /// <summary>
        /// Gets the member first name
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets the member last name
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets the member birth date
        /// </summary>
        [DataMember]
        public DateTime? BirthDate { get; set; }

        #endregion
    }
}
