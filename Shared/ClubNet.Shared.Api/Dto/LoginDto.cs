namespace ClubNet.Shared.Api.Dto
{
    /// <summary>
    /// Define the login model properties
    /// </summary>
    public sealed class LoginDto : ILoginModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user
        /// </summary>
        public bool RememberMe { get; set; }
     
        #endregion
    }
}
