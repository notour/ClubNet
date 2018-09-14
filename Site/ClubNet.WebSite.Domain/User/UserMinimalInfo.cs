using System;

namespace ClubNet.WebSite.Domain.User
{
    /// <summary>
    /// Store the user minimal informations
    /// </summary>
    public class UserMinimalInfo
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserMinimalInfo"/>
        /// </summary>
        public UserMinimalInfo(Guid id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        #endregion

        #region Propeties

        /// <summary>
        /// Gets the user unique identifier
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the user display name
        /// </summary>
        public string DisplayName { get; }

        #endregion
    }
}
