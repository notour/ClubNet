namespace ClubNet.WebSite.Controllers
{
    using ClubNet.Shared.Api.Contracts;
    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Common.Errors;
    using ClubNet.WebSite.Common.Resources;
    using ClubNet.WebSite.Domain.User;
    using ClubNet.WebSite.Middleware;
    using ClubNet.WebSite.Resources;
    using ClubNet.WebSite.Tools;
    using ClubNet.WebSite.ViewModels.Forms;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    /// <summary>
    /// Controller user to manager the accounts
    /// </summary>
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        #region Fields

        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly SignInManager<UserInfo> _signInManager;
        private readonly IClubDescriptor _clubDescriptor;
        private readonly IUserStore<UserInfo> _userStore;
        private readonly IEmailSender _emailSender;
        private readonly IApiService _apiService;
        private readonly IUserApi _userApi;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="AccountController"/>
        /// </summary>
        public AccountController(SignInManager<UserInfo> signInManager,
                                 IServiceProvider serviceProvider,
                                 IUserApi userApi,
                                 IApiService apiService,
                                 ILogger<AccountController> logger,
                                 IResourceService resourceService,
                                 IEmailSender emailSender,
                                 IClubDescriptor clubDescriptor,
                                 IEmailTemplateProvider emailTemplateProvider,
                                 IUserStore<UserInfo> userStore)
            : base(serviceProvider, logger, resourceService)
        {
            this._signInManager = signInManager;
            this._userApi = userApi;
            this._apiService = apiService;
            this._emailSender = emailSender;
            this._emailTemplateProvider = emailTemplateProvider;
            this._clubDescriptor = clubDescriptor;
            this._userStore = userStore;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called to access the login page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            InitializeCurrentViewInfo();

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
            InitializeCurrentViewInfo();

            if (_signInManager.IsSignedIn(HttpContext.User))
                return RedirectToPage("/" + RequestService.CurrentLanguage + "/");

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
        public async Task<IActionResult> RegisterForm([FromBody] RegisterDto dto, [FromBody]string gRecaptchaResponse)
        {
            InitializeCurrentViewInfo();
            var pageVM = new PageViewModel<RegisterFormVM>(RequestService);
            pageVM.ViewModel.Email = dto.Email;

            bool succeed = true;

            var returnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (dto.Password != dto.ConfirmationPassword)
                {
                    ModelState.AddModelError(nameof(PageViewModel<RegisterFormVM>.ViewModel) + "." + nameof(RegisterFormVM.ConfirmationPassword),
                                             this.ResourceService.GetString(ErrorCategory.Register, nameof(ErrorMessages.ConfirmationPasswordNotEquals), RequestService.CurrentLanguage));
                    succeed = false;
                }

                if (!await ReCaptcha.ValidateAsync(this._apiService.GetPrivateApiKey(Apis.ReCaptcha), gRecaptchaResponse))
                {
                    pageVM.ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Global, nameof(ErrorMessages.RecaptchaFailed), RequestService.CurrentLanguage));
                    succeed = false;
                }
                if (succeed)
                {
                    var user = new UserInfo(dto.Email) { Email = dto.Email, PreferredCulture = RequestService.CurrentLanguage };
                    var result = await this._signInManager.UserManager.CreateAsync(user, dto.Password);
                    if (result.Succeeded)
                    {
                        Logger.LogInformation(LogMessagesRes.UserAccountCreated.WithArguments(dto.Email));

                        var code = await this._signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = this.Url.Action(new UrlActionContext()
                        {
                            Action = nameof(ConfirmEmail),
                            Controller = "Account",
                            Values = new { userId = user.Id, code = code, lang = this.RequestService.CurrentLanguage },
                            Protocol = Request.Scheme
                        });

                        var subject = this._emailTemplateProvider.GetSubject(nameof(ConfirmEmail), RequestService.CurrentLanguage);

                        var htmlTemplate = this._emailTemplateProvider.GetTemplate(nameof(ConfirmEmail), RequestService.CurrentLanguage);

                        if (!string.IsNullOrEmpty(htmlTemplate))
                            await _emailSender.SendEmailAsync(dto.Email, subject, htmlTemplate.Replace("{{ConfirmUrl}}", (HtmlEncoder.Default.Encode(callbackUrl))));
                        else
                            await _emailSender.SendEmailAsync(dto.Email, "Confirm your email",
                                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        AddMessage(nameof(Resources.Controllers.AccountController.AccountCreated), ViewModels.MessageType.Success);
                        AddMessage(nameof(Resources.Controllers.AccountController.ConfirmationEmailSend), ViewModels.MessageType.Warning, dto.Email);

                        PassMessages();

                        return RedirectToAction(nameof(Login), pageVM);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Register, error.Code, RequestService.CurrentLanguage));
                    }
                }
            }

            return View(nameof(Register), pageVM);
        }

        /// <summary>
        /// Confirm the user token
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            InitializeCurrentViewInfo();

            var user = await this._userStore.FindByIdAsync(userId, RequestService.CancellationToken);
            var identity = await this._signInManager.UserManager.ConfirmEmailAsync(user, code);
            if (identity.Succeeded)
            {
                AddMessage(nameof(Resources.Controllers.AccountController.AccountCreated), ViewModels.MessageType.Success);
                await this._signInManager.SignInAsync(user, true);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            foreach (var error in identity.Errors)
            {
                ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Register, error.Code, RequestService.CurrentLanguage));
            }

            return RedirectToAction(nameof(Login));

        }

        #endregion
    }
}
