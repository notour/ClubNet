namespace ClubNet.WebSite.Controllers
{
    using ClubNet.Shared.Api.Contracts;
    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Domain.User;
    using ClubNet.WebSite.Middleware;
    using ClubNet.WebSite.ViewModels.Forms;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Controller user to manager the accounts
    /// </summary>
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        #region Fields

        private readonly SignInManager<UserInfo> _signInManager;
        private readonly IUserApi _userApi;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="AccountController"/>
        /// </summary>
        public AccountController(SignInManager<UserInfo> signInManager, IServiceProvider serviceProvider, IUserApi userApi)
            : base(serviceProvider)
        {
            _signInManager = signInManager;
            _userApi = userApi;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called to access the login page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }

            var pageVM = new PageViewModel<LoginFormVM>(RequestService);

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            pageVM.ViewModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            pageVM.ViewModel.ReturnUrl = returnUrl ?? Url.Content("~/");

            return View(nameof(Login), pageVM);
        }

        /// <summary>
        /// Called to access the register page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
                return RedirectToAction("Index", "Home");

            var pageVM = new PageViewModel<RegisterFormVM>(RequestService);

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View(nameof(Register), pageVM);
        }

        /// <summary>
        /// Called by the register form to create a new account
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterForm([FromBody] RegisterDto dto)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
