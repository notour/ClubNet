namespace ClubNet.WebSite.Domain.User
{
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Define the current season subscription
    /// </summary>
    [BsonDiscriminator]
    public sealed class Subscription : MemberSeasonEntity<UserInfoType>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="Subscription"/>
        /// </summary>
        public Subscription()
            : base(UserInfoType.Subscription)
        {
        }

        #endregion

        #region Properties



        #endregion
    }
}
