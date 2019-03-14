namespace ClubNet.WebSite.Domain.User
{
    using System.Runtime.Serialization;

    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Define a physical address on earth
    /// </summary>
    [DataContract]
    public sealed class PhysicalAddress
    {
        #region Properties

        /// <summary>
        /// Gets the address city name
        /// </summary>
        [DataMember]
        [BsonRequired]
        public string City { get; private set; }

        /// <summary>
        /// Gets the address city postal code
        /// </summary>
        [DataMember]
        [BsonRequired]
        public string PostalCode { get; private set; }

        /// <summary>
        /// Gets the address street
        /// </summary>
        [DataMember]
        [BsonRequired]
        public string Street { get; private set; }

        /// <summary>
        /// Gets the address complement information
        /// </summary>
        [DataMember]
        [BsonElement]
        public string ComplementStreet { get; private set; }

        ///// <summary>
        ///// Gets the latitude coordonate of the current address
        ///// </summary>
        //[DataMember]
        //public long Latitude { get; private set; }

        ///// <summary>
        ///// Gets the longitude coordonate of the current address
        ///// </summary>
        //[DataMember]
        //public long Longitude { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new <see cref="PhysicalAddress"/>
        /// </summary>
        public static PhysicalAddress Create(string city, string postalCode, string street, string complementStreet)
        {
            return SetupData(null, city, postalCode, street, complementStreet);
        }

        /// <summary>
        /// Update the current <see cref="PhysicalAddress"/> information
        /// </summary>
        public void Update(string city, string postalCode, string street, string complementStreet)
        {
            SetupData(this, city, postalCode, street, complementStreet);
        }

        /// <summary>
        /// Setup the <see cref="PhysicalAddress"/> address
        /// </summary>
        private static PhysicalAddress SetupData(PhysicalAddress instance,
                                                 string city,
                                                 string postalCode,
                                                 string street,
                                                 string complementStreet)
        {
            if (instance == null)
                instance = new PhysicalAddress();

            instance.City = city;
            instance.ComplementStreet = complementStreet;
            instance.PostalCode = postalCode;
            instance.Street = street;

            return instance;
        }

        #endregion
    }
}
