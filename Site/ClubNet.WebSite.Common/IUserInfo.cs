namespace ClubNet.WebSite.Common
{
    /// <summary>
    /// Contains that expose only the basic user information needed
    /// </summary>
    public interface IUserInfo
    {
        #region Properties

        /// <summary>
        /// Gets the login user informartion
        /// </summary>
        string Login { get; }

        /// <summary>
        /// Gets the user email address
        /// </summary>
        string Email { get; }

        #endregion
    }
}
