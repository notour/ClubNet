namespace ClubNet.WebSite.Controllers
{
    using ClubNet.Shared.Api.Contracts;
    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Domain.User;
    using ClubNet.WebSite.Middleware;
    using ClubNet.WebSite.Tools;
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
        private readonly IApiService _apiService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="AccountController"/>
        /// </summary>
        public AccountController(SignInManager<UserInfo> signInManager, IServiceProvider serviceProvider, IUserApi userApi, IApiService apiService)
            : base(serviceProvider)
        {
            _signInManager = signInManager;
            _userApi = userApi;
            _apiService = apiService;
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
        public async Task<IActionResult> RegisterForm([FromBody] RegisterDto dto, [FromForm]string gRecaptchaResponse)
        {
            var returnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (dto.Password != dto.ConfirmationPassword)
                {
                    ModelState.AddModelError(nameof(PageViewModel<RegisterFormVM>.ViewModel) + "." + nameof(RegisterFormVM.ConfirmationPassword), "Confirmation password and password not identical");
                    return View(nameof(Register));
                }

                if (await ReCaptcha.ValidateAsync(this._apiService.GetPrivateApiKey(Apis.ReCaptcha), gRecaptchaResponse))
                {
                    ModelState.AddModelError(string.Empty, "Recaptcha missing");
                    return View(nameof(Register));
                }
                var user = new UserInfo(dto.Email) { Email = dto.Email };
                var result = await _signInManager.UserManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(nameof(Register));
        }

        #endregion
    }
}
