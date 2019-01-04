namespace ClubNet.WebSite.Controllers
{
    using System;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using ClubNet.Shared.Api.Contracts;
    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Common.Errors;
    using ClubNet.WebSite.Common.Resources;
    using ClubNet.WebSite.Domain.User;
    using ClubNet.WebSite.Resources;
    using ClubNet.WebSite.Tools;
    using ClubNet.WebSite.ViewModels.Forms;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.Logging;

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

        #region Login

        /// <summary>
        /// Called to access the login page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            InitializeCurrentViewInfo();

            if (this._signInManager.IsSignedIn(this.HttpContext.User))
            {
                var user = await this._signInManager.UserManager.GetUserAsync(this.HttpContext.User);
                return LocalizedRedirect(returnUrl, user.PreferredCulture);
            }

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            var pageVM = new PageViewModel<LoginFormVM>(this.RequestService);

            pageVM.ViewModel.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            pageVM.ViewModel.ReturnUrl = returnUrl ?? this.Url.Content("~/");

            return View(nameof(Login), pageVM);
        }

        /// <summary>
        /// Called by the login form to authenticate
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> LoginForm([FromBody]LoginDto loginDto, [FromBody]string returnUrl)
        {
            InitializeCurrentViewInfo();

            if (this._signInManager.IsSignedIn(this.HttpContext.User))
                return RedirectToAction(nameof(Login), "Account", new { returnUrl = returnUrl });

            var signInResult = await this._signInManager.PasswordSignInAsync(loginDto.Login, loginDto.Password, loginDto.RememberMe, true);
            if (signInResult.Succeeded)
                return RedirectToAction(nameof(Login), "Account", new { returnUrl = returnUrl });

            if (signInResult.IsLockedOut)
                this.ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Login, nameof(ErrorMessages.AccountLocked), this.RequestService.CurrentLanguage));
            else if (signInResult.IsNotAllowed)
            {
                var user = await this._signInManager.UserManager.FindByNameAsync(loginDto.Login);
                if (user != null && await this._signInManager.UserManager.IsEmailConfirmedAsync(user) == false)
                    this.ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Login, nameof(ErrorMessages.EmailConfirmationRequired), this.RequestService.CurrentLanguage));
            }
            else if (signInResult.RequiresTwoFactor)
                this.ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Login, nameof(ErrorMessages.EmailConfirmationRequired), this.RequestService.CurrentLanguage));

            this.ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Login, nameof(ErrorMessages.LoginFailed), this.RequestService.CurrentLanguage));

            SaveModelState();

            return RedirectToAction(nameof(Login), "Account");
        }

        #endregion

        #region Register

        /// <summary>
        /// Called to access the register page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            InitializeCurrentViewInfo();

            if (this._signInManager.IsSignedIn(this.HttpContext.User))
                return LocalizedRedirect(string.Empty, this.RequestService.CurrentLanguage);

            var pageVM = new PageViewModel<RegisterFormVM>(this.RequestService);

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

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

            bool succeed = true;

            string returnUrl = this.Url.Content("~/");
            if (this.ModelState.IsValid)
            {
                if (dto.Password != dto.ConfirmationPassword)
                {
                    this.ModelState.AddModelError(nameof(RegisterFormVM.ConfirmationPassword),
                                             this.ResourceService.GetString(ErrorCategory.Register, nameof(ErrorMessages.ConfirmationPasswordNotEquals), this.RequestService.CurrentLanguage));
                    succeed = false;
                }

                if (!await ReCaptcha.ValidateAsync(this._apiService.GetPrivateApiKey(Apis.ReCaptcha), gRecaptchaResponse))
                {
                    this.ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Global, nameof(ErrorMessages.RecaptchaFailed), this.RequestService.CurrentLanguage));
                    succeed = false;
                }
                if (succeed)
                {
                    var user = new UserInfo(dto.Email) { Email = dto.Email, PreferredCulture = this.RequestService.CurrentLanguage };
                    var result = await this._signInManager.UserManager.CreateAsync(user, dto.Password);
                    if (result.Succeeded)
                    {
                        this.Logger.LogInformation(LogMessagesRes.UserAccountCreated.WithArguments(dto.Email));

                        string code = await this._signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
                        string callbackUrl = this.Url.Action(new UrlActionContext()
                        {
                            Action = nameof(ConfirmEmail),
                            Controller = "Account",
                            Values = new { userId = user.Id, code = code, lang = this.RequestService.CurrentLanguage },
                            Protocol = this.Request.Scheme
                        });

                        string subject = this._emailTemplateProvider.GetSubject(nameof(ConfirmEmail), this.RequestService.CurrentLanguage);

                        string htmlTemplate = this._emailTemplateProvider.GetTemplate(nameof(ConfirmEmail), this.RequestService.CurrentLanguage);

                        if (!string.IsNullOrEmpty(htmlTemplate))
                            await this._emailSender.SendEmailAsync(dto.Email, subject, htmlTemplate.Replace("{{ConfirmUrl}}", (HtmlEncoder.Default.Encode(callbackUrl))));
                        else
                            await this._emailSender.SendEmailAsync(dto.Email, "Confirm your email",
                                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        AddMessage(nameof(Resources.Controllers.AccountController.AccountCreated), ViewModels.MessageType.Success);
                        AddMessage(nameof(Resources.Controllers.AccountController.ConfirmationEmailSend), ViewModels.MessageType.Warning, dto.Email);

                        SaveMessages();

                        return RedirectToAction(nameof(Login));
                    }

                    AddErrors(result, ErrorCategory.Register);
                }
            }

            SaveModelState();

            return RedirectToAction(nameof(Register), "Account");
        }

        /// <summary>
        /// Confirm the user token
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            InitializeCurrentViewInfo();

            var user = await this._userStore.FindByIdAsync(userId, this.RequestService.CancellationToken);
            if (user == null)
                return NotFound();

            var identity = await this._signInManager.UserManager.ConfirmEmailAsync(user, code);
            if (identity.Succeeded)
            {
                AddMessage(nameof(Resources.Controllers.AccountController.AccountCreated), ViewModels.MessageType.Success);
                await this._signInManager.SignInAsync(user, true);

                SaveMessages();

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            AddErrors(identity, ErrorCategory.User);

            return RedirectToAction(nameof(Login));

        }

        /// <summary>
        /// View to generate link of forgotten password
        /// </summary>
        [HttpGet]
        public IActionResult ForgottenPassword()
        {
            InitializeCurrentViewInfo();
            return View(nameof(ForgottenPassword));
        }

        /// <summary>
        /// View to reset you forgotten password
        /// </summary>
        [HttpGet]
        public IActionResult ResetForgottenPassword(string email, string resetToken)
        {
            InitializeCurrentViewInfo();

            var pageVM = new PageViewModel<ChangePasswordFormVM>(RequestService);
            pageVM.ViewModel.ResetToken = resetToken;
            pageVM.ViewModel.Email = email;

            return View(nameof(ResetForgottenPassword), pageVM);
        }

        /// <summary>
        /// Reset the user password
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody]ChangePasswordDto dto, [FromBody]string returlUrl)
        {
            InitializeCurrentViewInfo();

            UserInfo user = null;

            if (dto.NewPassword != dto.ConfirmationPassword)
            {
                this.ModelState.AddModelError(nameof(dto.ConfirmationPassword),
                                              this.ResourceService.GetString(ErrorCategory.Register, nameof(ErrorMessages.ConfirmationPasswordNotEquals), this.RequestService.CurrentLanguage));
            }
            else
            {
                IdentityResult changeResult = null;
                if (this._signInManager.IsSignedIn(HttpContext.User))
                {
                    user = await this._signInManager.UserManager.GetUserAsync(HttpContext.User);
                    changeResult = await this._signInManager.UserManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
                }
                else
                {
                    user = await this._signInManager.UserManager.FindByEmailAsync(dto.Email);
                    if (user == null)
                        this.ModelState.AddModelError("Email", this.ResourceService.GetString(nameof(ErrorMessages.EmailNotFound), this.RequestService.CurrentLanguage).WithArguments(dto.Email));
                    else
                        changeResult = await this._signInManager.UserManager.ResetPasswordAsync(user, dto.ResetToken, dto.NewPassword);
                }

                if (changeResult != null && changeResult.Succeeded)
                {
                    AddMessage(nameof(Resources.Controllers.AccountController.PasswordReset), ViewModels.MessageType.Success);

                    SaveMessages();

                    if (string.IsNullOrEmpty(returlUrl))
                        return RedirectToAction(nameof(Login), "Account");
                    return LocalRedirect(returlUrl);
                }
                else if (changeResult != null)
                    AddErrors(changeResult, ErrorCategory.User);
            }

            SaveModelState();

            return RedirectToAction(nameof(ResetForgottenPassword), "Account", new { resetToken = dto.ResetToken, email = dto.Email });
        }

        /// <summary>
        /// Called by the form to generate the reset password token
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ForgottenPasswordForm([FromBody]string email)
        {
            InitializeCurrentViewInfo();

            var user = await this._signInManager.UserManager.FindByEmailAsync(email);

            if (user == null)
            {
                this.ModelState.AddModelError("Email", this.ResourceService.GetString(nameof(ErrorMessages.EmailNotFound), this.RequestService.CurrentLanguage).WithArguments(email));

                SaveModelState();
            }
            else
            {
                string callbackUrl = this.Url.Action(new UrlActionContext()
                {
                    Action = nameof(ResetForgottenPassword),
                    Controller = "Account",
                    Values = new { email = user.Email, resetToken = await this._signInManager.UserManager.GeneratePasswordResetTokenAsync(user), lang = this.RequestService.CurrentLanguage },
                    Protocol = this.Request.Scheme
                });

                string subject = this._emailTemplateProvider.GetSubject(nameof(ResetForgottenPassword), this.RequestService.CurrentLanguage);

                string htmlTemplate = this._emailTemplateProvider.GetTemplate(nameof(ResetForgottenPassword), this.RequestService.CurrentLanguage);

                if (!string.IsNullOrEmpty(htmlTemplate))
                    await this._emailSender.SendEmailAsync(email, subject, htmlTemplate.Replace("{{resetForgottenUrl}}", (HtmlEncoder.Default.Encode(callbackUrl))));

                AddMessage(nameof(Resources.Controllers.AccountController.ResetAccountEmailSend), ViewModels.MessageType.Success, email);

                SaveMessages();
            }

            return RedirectToAction(nameof(Login), "Account");
        }

        #endregion

        #region Logout

        /// <summary>
        /// Logout the current user
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            InitializeCurrentViewInfo();
            await this._signInManager.SignOutAsync();
            return LocalizedRedirect(string.Empty, this.RequestService.CurrentLanguage);
        }

        #endregion

        #region Tools

        /// <summary>
        /// Add the errors to the model state
        /// </summary>
        private void AddErrors(IdentityResult identity, ErrorCategory errorCategory)
        {
            foreach (var error in identity.Errors)
            {
                this.ModelState.AddModelError(string.Empty, this.ResourceService.GetString(errorCategory, error.Code, this.RequestService.CurrentLanguage));
            }
        }

        #endregion

        #endregion
    }
}
