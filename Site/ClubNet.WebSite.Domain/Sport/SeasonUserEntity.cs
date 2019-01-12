namespace ClubNet.WebSite.Domain.Sport
{
    using System;
    using System.Runtime.Serialization;
    using ClubNet.WebSite.Common;
    using ClubNet.WebSite.Domain.Security;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Base class of the saison entities
    /// </summary>
    [DataContract]
    public abstract class SeasonUserEntity<TEntityType> : UserEntity<TEntityType>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="SeasonUserEntity{TEntityType}"/>
        /// </summary>
        public SeasonUserEntity(TEntityType entityType)
            : base(entityType)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the season unique id
        /// </summary>
        [DataMember]
        [BsonElement]
        public Guid SeasonId { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Update the current entity
        /// </summary>
        protected void Create(Guid seasonId, IUserInfo userInfo, SecurityCriteria securityCriteria)
        {
            base.Create(userInfo, securityCriteria);
            this.SeasonId = seasonId;
        }

        #endregion
    }
}
