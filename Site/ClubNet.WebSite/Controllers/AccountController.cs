using ClubNet.WebSite.Domain.User;
using ClubNet.WebSite.Middleware;
using ClubNet.WebSite.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubNet.WebSite.Controllers
{
    /// <summary>
    /// Controller user to manager the accounts
    /// </summary>
    [AllowAnonymous]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class AccountController : Controller
    {
        #region Fields

        private readonly SignInManager<UserInfo> _signInManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="AccountController"/>
        /// </summary>
        public AccountController(SignInManager<UserInfo> signInManager)
        {
            _signInManager = signInManager;
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

            var vm = new LoginPageVM();

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            vm.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            vm.ReturnUrl = returnUrl ?? Url.Content("~/");

            return View(nameof(Login), vm);
        }

        #endregion
    }
}
