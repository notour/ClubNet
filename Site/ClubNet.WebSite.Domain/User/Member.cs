namespace ClubNet.WebSite.Domain.User
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using ClubNet.WebSite.Common;
    using ClubNet.WebSite.Domain.Interfaces;
    using ClubNet.WebSite.Domain.Security;
    using ClubNet.WebSite.Domain.Sport;

    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Define a unique document by the phoenix club memeber
    /// </summary>
    [DataContract]
    [BsonDiscriminator]
    public sealed class Member : SeasonUserEntity<UserInfoType>, IEntity<UserInfoType>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="Member"/>
        /// </summary>
        public Member()
            : base(UserInfoType.Member)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique member name
        /// </summary>
        [BsonRequired]
        [DataMember(IsRequired = true)]
        public string MemberId { get; private set; }

        /// <summary>
        /// Gets the member first name
        /// </summary>
        [BsonRequired]
        [DataMember(IsRequired = true)]
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the member last name
        /// </summary>
        [BsonRequired]
        [DataMember(IsRequired = true)]
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the member birthday
        /// </summary>
        [BsonRequired]
        [DataMember(IsRequired = true)]
        public DateTime BirthDay { get; private set; }

        /// <summary>
        /// Gets the member birth place info
        /// </summary>
        [DataMember()]
        [BsonElement]
        public string BirthPlace { get; private set; }

        /// <summary>
        /// Gets the current <see cref="ContactInfo"/>
        /// </summary>
        [DataMember]
        [BsonRequired]
        public ContactInfo ContactInfo { get; private set; }

        /// <summary>
        /// Gets the current member physical address
        /// </summary>
        [DataMember]
        [BsonRequired]
        public PhysicalAddress PhysicalAddress { get; private set; }

        /// <summary>
        /// Gets the path of the current photo associate to the member in the club
        /// </summary>
        [DataMember()]
        [BsonElement]
        public ImageFileInfo PhotoClubPath { get; private set; }

        /// <summary>
        /// Gets the current identity photo path
        /// </summary>
        [DataMember()]
        [BsonElement]
        public ImageFileInfo IdentifyPhotoPath { get; private set; }

        /// <summary>
        /// Gets a value indicating the member sexe
        /// </summary>
        [DataMember()]
        [BsonElement]
        public SexeEnum Sexe { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new member 
        /// </summary>
        public static Member Create(string firstName,
                                    string lastName,
                                    DateTime birthDay,
                                    string birthPlace,
                                    SexeEnum sexe,
                                    ContactInfo contactInfo,
                                    PhysicalAddress physicalAddress,
                                    ImageFileInfo photoClubPath,
                                    ImageFileInfo identityPhotoPath,
                                    IEnumerable<IUserInfo> owners,
                                    SecurityCriteria securityCriteria,
                                    Guid seasonId,
                                    string memberIdSuffix,
                                    bool isDraft)
        {
            return SetupData(null,
                             firstName,
                             lastName,
                             birthDay,
                             birthPlace,
                             sexe,
                             contactInfo,
                             physicalAddress,
                             photoClubPath,
                             identityPhotoPath,
                             owners,
                             securityCriteria,
                             seasonId,
                             memberIdSuffix,
                             isDraft);
        }

        /// <summary>
        /// Update the current member info
        /// </summary>
        public void Update(ContactInfo contactInfo,
                           PhysicalAddress physicalAddress,
                           ImageFileInfo photoClubPath,
                           ImageFileInfo identityPhotoPath,
                           SecurityCriteria securityCriteria,
                           bool isDraft)
        {
            SetupData(null,
                     this.FirstName,
                     this.LastName,
                     this.BirthDay,
                     this.BirthPlace,
                     this.Sexe,
                     contactInfo,
                     physicalAddress,
                     photoClubPath,
                     identityPhotoPath,
                     null,
                     securityCriteria,
                     this.SeasonId,
                     string.Empty,
                     isDraft);
        }

        /// <summary>
        /// Setup the current member data
        /// </summary>
        private static Member SetupData(Member instance,
                                        string firstName,
                                        string lastName,
                                        DateTime birthDay,
                                        string birthPlace,
                                        SexeEnum sexe,
                                        ContactInfo contactInfo,
                                        PhysicalAddress physicalAddress,
                                        ImageFileInfo photoClubPath,
                                        ImageFileInfo identityPhotoPath,
                                        IEnumerable<IUserInfo> owners,
                                        SecurityCriteria securityCriteria,
                                        Guid seasonId,
                                        string memberIdSuffix,
                                        bool isDraft)
        {
            if (instance == null)
            {
                instance = new Member();
                instance.Create(seasonId, owners, securityCriteria, isDraft);
                instance.MemberId = GenerateMemberId(firstName, lastName, birthDay, memberIdSuffix);
            }
            else
            {
                instance.Update(securityCriteria, isDraft);
            }

            instance.Sexe = sexe;
            instance.FirstName = firstName;
            instance.LastName = lastName;
            instance.BirthDay = birthDay;
            instance.BirthPlace = birthPlace;
            instance.ContactInfo = contactInfo;
            instance.PhysicalAddress = physicalAddress;
            instance.PhotoClubPath = photoClubPath;
            instance.IdentifyPhotoPath = identityPhotoPath;

            return instance;
        }

        /// <summary>
        /// Generate the member id from the member fixed information
        /// </summary>
        public static string GenerateMemberId(string firstName,
                                              string lastName,
                                              DateTime birthDay,
                                              string memberIdSuffix)
        {
            var memberId = birthDay.ToString("yyyyMMDD") + "-" + lastName.ToUpper().Substring(0, 3) + firstName.ToUpper().Substring(0, 3);
            if (!string.IsNullOrEmpty(memberIdSuffix))
                memberId += "-" + memberIdSuffix.ToUpper();

            return memberId;
        }

        #endregion
    }
}
