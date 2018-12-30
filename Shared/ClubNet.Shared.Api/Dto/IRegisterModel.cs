namespace ClubNet.Shared.Api.Dto
{
    /// <summary>
    /// Define the register model properties
    /// </summary>
    public interface IRegisterModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the user confirmation password
        /// </summary>
        string ConfirmationPassword { get; set; }
     
        #endregion
    }
}
