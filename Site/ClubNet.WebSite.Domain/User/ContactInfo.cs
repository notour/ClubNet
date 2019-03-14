namespace ClubNet.WebSite.Domain.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Define the contact info
    /// </summary>
    [DataContract]
    public sealed class ContactInfo
    {
        #region Properties

        /// <summary>
        /// Gets the contact email informations
        /// </summary>
        [DataMember]
        [DataType(DataType.EmailAddress)]
        [BsonElement]
        public string Email { get; private set; }

        /// <summary>
        /// Gets the contact phone number
        /// </summary>
        [DataMember]
        [DataType(DataType.PhoneNumber)]
        [BsonElement]
        public string GSM { get; private set; }

        /// <summary>
        /// Gets the facebook profil info
        /// </summary>
        [DataMember]
        [BsonElement]
        [BsonIgnoreIfDefault]
        public string FacebookProfile { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new <see cref="ContactInfo"/>
        /// </summary>
        public static ContactInfo Create(string email,
                                         string gsm,
                                         string facebookProfile)
        {
            return SetupData(null, email, gsm, facebookProfile);
        }

        /// <summary>
        /// Update the current contact info
        /// </summary>
        public void Update(string email,
                           string gsm,
                           string facebookProfile)
        {
            SetupData(this, email, gsm, facebookProfile);
        }


        /// <summary>
        /// Setup the current <see cref="ContactInfo"/> data
        /// </summary>
        private static ContactInfo SetupData(ContactInfo instance,
                                             string email,
                                             string gsm,
                                             string facebookProfile)
        {
            if (instance == null)
                instance = new ContactInfo();

            instance.Email = email;
            instance.GSM = gsm;
            instance.FacebookProfile = facebookProfile;

            return instance;
        }

        #endregion
    }
}
