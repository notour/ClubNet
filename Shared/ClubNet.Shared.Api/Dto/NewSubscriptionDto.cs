namespace ClubNet.Shared.Api.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class NewSubscriptionBaseDto : INewSubscriptionBaseDto
    {
        #region Properties

        ///// <summary>
        ///// Gets a value indicating the subscription season id
        ///// </summary>
        //[DataMember]
        //public Guid SeasonId { get; set; }

        ///// <summary>
        ///// Gets the member first name
        ///// </summary>
        //[DataMember]
        //public string FirstName { get; set; }

        ///// <summary>
        ///// Gets the member last name
        ///// </summary>
        //[DataMember]
        //public string LastName { get; set; }

        ///// <summary>
        ///// Gets the member birth date
        ///// </summary>
        //[DataMember]
        //public DateTime? BirthDate { get; set; }


        /// <summary>
        /// Gets the member id target by this current subscription
        /// </summary>
        [DataMember]
        public Guid? MemberId { get; set; }

        /// <summary>
        /// Gets the current season associate to the current subscription
        /// </summary>
        [DataMember]
        public Guid SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the member sexe
        /// </summary>
        [DataMember]
        public SexeEnumDto Sexe { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        [DataMember]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the member birth place
        /// </summary>
        [DataMember]
        public string BirthPlace { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mail address
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the address street
        /// </summary>
        [DataMember]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the address complement information
        /// </summary>
        [DataMember]
        public string StreetComplement { get; set; }

        /// <summary>
        /// Gets or sets the current postal code
        /// </summary>
        [DataMember]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the member city 
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets values indicating the member authorization of data usage
        /// </summary>
        [DataMember]
        public DataUsageRightFormDto DataUsageRight { get; set; }

        /// <summary>
        /// Gets or sets the role collections
        /// </summary>
        [DataMember]
        public IEnumerable<SubscriptionRoleEnumDto> Roles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the current subscribption concern a competition player
        /// </summary>
        [DataMember]
        public bool IsCompetitionPlayer { get; set; }

        #endregion
    }
}
