namespace ClubNet.Shared.Api.Dto
{
    /// <summary>
    /// Dto to changed the current password
    /// </summary>
    public class ChangePasswordDto : IChangePasswordModel
    {
        /// <summary>
        /// Gets or sets the input reset token if exist
        /// </summary>
        public string ResetToken { get; set; }

        /// <summary>
        /// Gets or sets the input reset token if exist
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the input old password
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the input new password
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the input password
        /// </summary>
        public string ConfirmationPassword { get; set; }
    }
}
