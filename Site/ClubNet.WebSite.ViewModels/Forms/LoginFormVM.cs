namespace ClubNet.WebSite.ViewModels.Forms
{
    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.Common.Contracts;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define the view model dedicated to the login page
    /// </summary>
    public sealed class LoginFormVM : BaseFormVM, ILoginModel
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="LoginPageVM"/>
        /// </summary>
        public LoginFormVM(IRequestService requestService)
            : base(requestService)
        {
            RememberMe = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the input login
        /// </summary>
        [BindProperty]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the input password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the current authentication profile must be saved
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Gets or sets the external login scheme availabled
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// Gets or sets the return url used to redirect after authentification
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets the error message to display
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        #endregion
    }
}
