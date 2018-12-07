//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//using ClubNet.WebSite.BusinessLayer.Contracts;
//using ClubNet.WebSite.Containers;

//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.Extensions.Logging;

//namespace ClubNet.WebSite.ViewModels
//{
//    [AllowAnonymous]
//    public class LoginPageVM : PageModel
//    {
//        #region Fields

//        private readonly ILogger<LoginPageVM> _logger;
//        private readonly IUserBL _userBL;

//        #endregion

//        #region Ctor

//        /// <summary>
//        /// Initialize a new instance of the class <see cref="LoginPageVM"/>
//        /// </summary>
//        public LoginPageVM(IUserBL userBL, ILogger<LoginPageVM> logger)
//        {
//            _userBL = userBL;
//            _logger = logger;
//        }

//        #endregion

//        #region Properties

//        /// <summary>
//        /// Gets or sets the input data container
//        /// </summary>
//        [BindProperty]
//        public LoginInfoContainer Input { get; set; }

//        /// <summary>
//        /// Gets or sets the external login scheme availabled
//        /// </summary>
//        public IList<AuthenticationScheme> ExternalLogins { get; set; }

//        /// <summary>
//        /// Gets or sets the return url used to redirect after authentification
//        /// </summary>
//        public string ReturnUrl { get; set; }

//        /// <summary>
//        /// Gets or sets the error message to display
//        /// </summary>
//        [TempData]
//        public string ErrorMessage { get; set; }

//        #endregion

//        #region Methods

//        public async Task OnGetAsync(string returnUrl = null)
//        {
//            if (!string.IsNullOrEmpty(ErrorMessage))
//            {
//                ModelState.AddModelError(string.Empty, ErrorMessage);
//            }

//            returnUrl = returnUrl ?? Url.Content("~/");

//            // Clear the existing external cookie to ensure a clean login process
//            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

//            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

//            ReturnUrl = returnUrl;
//        }

//        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
//        {
//            returnUrl = returnUrl ?? Url.Content("~/");

//            if (ModelState.IsValid)
//            {
//                // This doesn't count login failures towards account lockout
//                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
//                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
//                if (result.Succeeded)
//                {
//                    _logger.LogInformation("User logged in.");
//                    return LocalRedirect(returnUrl);
//                }
//                if (result.RequiresTwoFactor)
//                {
//                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
//                }
//                if (result.IsLockedOut)
//                {
//                    _logger.LogWarning("User account locked out.");
//                    return RedirectToPage("./Lockout");
//                }
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//                    return Page();
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return Page();
//        }

//        #endregion
//    }
//}
