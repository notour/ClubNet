namespace ClubNet.WebSite.ViewModels.Forms
{
    using System.ComponentModel.DataAnnotations;
    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.Common.Contracts;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Define the view model dedicated to the change password page
    /// </summary>
    public sealed class ChangePasswordFormVM : BaseFormVM, IChangePasswordModel
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="LoginPageVM"/>
        /// </summary>
        public ChangePasswordFormVM(IRequestService requestService)
            : base(requestService)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the input reset token if exist
        /// </summary>
        [BindProperty]
        public string ResetToken { get; set; }

        /// <summary>
        /// Gets or sets the input reset token if exist
        /// </summary>
        [BindProperty]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the input old password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the input new password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        /// <summary>
        /// Gets or sets the input password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmationPassword { get; set; }

        #endregion
    }
}
