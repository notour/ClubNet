namespace ClubNet.WebSite.Domain.User
{
    /// <summary>
    /// Define the current season subscription
    /// </summary>
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
