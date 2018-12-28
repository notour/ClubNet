namespace ClubNet.WebSite.ViewModels.Forms
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Define the view model dedicated to the login page
    /// </summary>
    public sealed class LoginFormVM : BaseFormVM
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="LoginPageVM"/>
        /// </summary>
        public LoginFormVM(IRequestService requestService)
            : base(requestService)
        {
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

        #region Methods

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    if (!string.IsNullOrEmpty(ErrorMessage))
        //    {
        //        ModelState.AddModelError(string.Empty, ErrorMessage);
        //    }

        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    // Clear the existing external cookie to ensure a clean login process
        //    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        //    ReturnUrl = returnUrl;
        //}

        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    if (ModelState.IsValid)
        //    {
        //        // This doesn't count login failures towards account lockout
        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("User logged in.");
        //            return LocalRedirect(returnUrl);
        //        }
        //        if (result.RequiresTwoFactor)
        //        {
        //            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        //        }
        //        if (result.IsLockedOut)
        //        {
        //            _logger.LogWarning("User account locked out.");
        //            return RedirectToPage("./Lockout");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return Page();
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return Page();
        //}

        #endregion
    }
}
