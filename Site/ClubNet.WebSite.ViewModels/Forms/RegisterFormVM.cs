namespace ClubNet.WebSite.ViewModels.Forms
{
    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.Common.Contracts;

    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define the register form need to create a user account on the club net system
    /// </summary>
    public sealed class RegisterFormVM : BaseFormVM, IRegisterModel
    {
        #region Ctor

        /// <summary>
        /// Define the 
        /// </summary>
        /// <param name="requestService"></param>
        public RegisterFormVM(IRequestService requestService) 
            : base(requestService)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the email account
        /// </summary>
        [Required]
        [EmailAddress()]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user confirmation password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmationPassword { get; set; }

        #endregion

    }
}
