namespace ClubNet.WebSite.Domain.User
{
    using System;
    using System.Runtime.Serialization;
    using ClubNet.WebSite.Domain.Interfaces;
    using ClubNet.WebSite.Domain.Sport;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Define a unique document by the phoenix club memeber
    /// </summary>
    [DataContract]
    public abstract class MemberSeasonEntity<TEntityType> : Entity<TEntityType>, ISeasonMemberEntity<TEntityType>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="Member"/>
        /// </summary>
        public MemberSeasonEntity(TEntityType entityType)
            : base(entityType)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the member first name
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [DataMember(IsRequired = true)]
        public Guid SeasonId { get; private set; }

        /// <summary>
        /// Gets the member id
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [DataMember(IsRequired = true)]
        public Guid MemberId { get; private set; }

        #endregion

        #region Methods
        #endregion
    }
}
