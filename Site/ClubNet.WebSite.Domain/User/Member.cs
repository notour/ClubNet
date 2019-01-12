namespace ClubNet.WebSite.Domain.User
{
    using System;
    using ClubNet.WebSite.Domain.Interfaces;
    using ClubNet.WebSite.Domain.Sport;

    /// <summary>
    /// Define a unique document by the phoenix club memeber
    /// </summary>
    public sealed class Member : UserEntity<UserInfoType>, IEntity<UserInfoType>
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
        /// Gets the member first name
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the member last name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the member birthday
        /// </summary>
        public DateTime Birthday { get; private set; }

        /// <summary>
        /// Gets the path of the current photo associate to the member in the club
        /// </summary>
        public ImageFileInfo PhotoClubPath { get; private set; }

        /// <summary>
        /// Gets the current identity photo path
        /// </summary>
        public ImageFileInfo IdentifyPhotoPath { get; private set; }

        #endregion

        #region Methods
        #endregion
    }
}
