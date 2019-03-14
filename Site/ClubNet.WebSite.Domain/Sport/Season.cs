namespace ClubNet.WebSite.Domain.Sport
{
    using System;
    using System.Runtime.Serialization;

    using ClubNet.WebSite.Domain.Security;

    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Define the season anchor
    /// </summary>
    [DataContract]
    public sealed class Season : Entity<SportEntityType>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref=""/>
        /// </summary>
        public Season()
            : base(SportEntityType.Saison)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the season start date
        /// </summary>
        [BsonElement]
        [DataMember]
        public DateTime Start { get; private set; }

        /// <summary>
        /// Gets the season end date
        /// </summary>
        [BsonElement]
        [DataMember]
        public DateTime End { get; private set; }

        /// <summary>
        /// Gets the value of the date when the subscription are opened
        /// </summary>
        public DateTime SubscriptionOpenDate { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new season object
        /// </summary>
        public static Season Create(DateTime start, DateTime end, DateTime subscriptionOpenDate, SecurityCriteria securityCriteria, bool isDraft)
        {
            var inst = new Season();
            inst.Create(securityCriteria, isDraft);
            inst.SetData(start, end, subscriptionOpenDate);
            return inst;
        }

        /// <summary>
        /// Create a new season object
        /// </summary>
        public void Update(DateTime start, DateTime end, DateTime subscriptionOpenDate, SecurityCriteria securityCriteria, bool isDraft)
        {
            this.Update(securityCriteria, isDraft);
            this.SetData(start, end, subscriptionOpenDate);
        }

        /// <summary>
        /// Setup the season data
        /// </summary>
        private void SetData(DateTime start, DateTime end, DateTime subscriptionOpenDate)
        {
            this.End = end;
            this.Start = start;
            this.SubscriptionOpenDate = subscriptionOpenDate;
        }

        #endregion
    }
}
