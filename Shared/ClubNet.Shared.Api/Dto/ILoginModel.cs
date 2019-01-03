namespace ClubNet.Shared.Api.Dto
{
    /// <summary>
    /// Define the login model properties
    /// </summary>
    public interface ILoginModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the login
        /// </summary>
        string Login { get; }

        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets or sets the user
        /// </summary>
        bool RememberMe { get; }
     
        #endregion
    }
}
