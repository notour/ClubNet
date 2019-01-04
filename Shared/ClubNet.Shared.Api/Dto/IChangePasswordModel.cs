namespace ClubNet.Shared.Api.Dto
{
    /// <summary>
    /// Model to change the password
    /// </summary>
    public interface IChangePasswordModel
    {
        /// <summary>
        /// Gets or sets the input reset token if exist
        /// </summary>
        string ResetToken { get; set; }

        /// <summary>
        /// Gets or sets the input reset token if exist
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the input old password
        /// </summary>
        string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the input new password
        /// </summary>
        string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the input password
        /// </summary>
        string ConfirmationPassword { get; set; }
    }
}
