namespace ClubNet.WebSite.Domain.Sport
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;
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

        #endregion

        #region Methods

        /// <summary>
        /// Create a new season object
        /// </summary>
        public static Season Create(DateTime start, DateTime end, SecurityCriteria securityCriteria)
        {
            var inst = new Season();
            inst.Create(securityCriteria);
            inst.SetData(start, end);
            return inst;
        }

        /// <summary>
        /// Create a new season object
        /// </summary>
        public void Update(DateTime start, DateTime end, SecurityCriteria securityCriteria)
        {
            this.Update(securityCriteria);
            this.SetData(start, end);
        }

        /// <summary>
        /// Setup the season data
        /// </summary>
        private void SetData(DateTime start, DateTime end)
        {
            this.End = end;
            this.Start = start;
        }

        #endregion
    }
}
