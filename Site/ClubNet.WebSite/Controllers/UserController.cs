namespace ClubNet.WebSite.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Common.Errors;
    using ClubNet.WebSite.Resources;
    using ClubNet.WebSite.Tools;
    using ClubNet.WebSite.ViewModels;
    using ClubNet.WebSite.ViewModels.Forms.User;
    using ClubNet.WebSite.ViewModels.User;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// User controller used to manipulate all the user and suscriptions
    /// </summary>
    [Authorize]
    public sealed class UserController : BaseController
    {
        #region Fields

        private readonly IApiService _apiService;
        private readonly IMenuBL _menuBL;
        private readonly IUserBL _userBL;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserController"/>
        /// </summary>
        public UserController(IServiceProvider serviceProvider,
                              ILogger<UserController> logger,
                              IResourceService resourceService,
                              IMenuBL menuBL,
                              IUserBL userBL,
                              IApiService apiService)
            : base(serviceProvider, logger, resourceService)
        {
            this._menuBL = menuBL;
            this._userBL = userBL;
            this._apiService = apiService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Display the profile user
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pageVm = new MultiPartPageViewModel(this.RequestService);

            await InitializeCommonViewModelAsync(pageVm);

            pageVm.AddViewModel("UserDashboard", (BaseVM)null);

            return View("_UserLayout", pageVm);
        }

        /// <summary>
        /// Display the subscriptions  user
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Subscriptions()
        {
            var timeout = base.RequestService.CancellationToken;
            var pageVm = new MultiPartPageViewModel(this.RequestService);

            await InitializeCommonViewModelAsync(pageVm);

            pageVm.AddViewModel<UserSubscriptionViewModel>("UserSubscriptions", await this._userBL.GetUserSubscriptionsAsync(timeout));

            return View("_UserLayout", pageVm);
        }

        /// <summary>
        /// Display the new inscription page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> NewSubscription()
        {
            var timeout = base.RequestService.CancellationToken;
            var pageVm = new MultiPartPageViewModel(this.RequestService);

            await InitializeCommonViewModelAsync(pageVm);

            pageVm.AddViewModel<NewSubscriptionFormVM>("NewSubscription", await this._userBL.GetNewSubscriptionFormVMAsync(timeout));

            return View("_UserLayout", pageVm);
        }

        /// <summary>
        /// Result of the current new subscription
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SubmitNewSubscriptionForm([FromBody] NewSubscriptionDto newSubscriptionDto, [FromBody]string gRecaptchaResponse, [FromBody]bool submitSubscription)
        {
            InitializeCurrentViewInfo();

            bool succeed = true;

            if (ModelState.IsValid)
            {
                if (!await ReCaptcha.ValidateAsync(this._apiService.GetPrivateApiKey(Apis.ReCaptcha), gRecaptchaResponse))
                {
                    this.ModelState.AddModelError(string.Empty, this.ResourceService.GetString(ErrorCategory.Global, nameof(ErrorMessages.RecaptchaFailed), this.RequestService.CurrentLanguage));
                    succeed = false;
                }
            }

            if (succeed)
            {
                await this._userBL.SaveNewSubscriptionAsync(newSubscriptionDto, !submitSubscription, base.RequestService.CancellationToken);

                SaveMessages();

                if (submitSubscription)
                    return LocalizedRedirectToAction("user", nameof(Subscriptions));
            }

            SaveModelState();

            return RedirectToAction(nameof(NewSubscription), "User");
        }

        /// <summary>
        /// Call to complet the <see cref="NewSubscriptionFormVM"/> with previous subscription
        /// </summary>
        [HttpPost]
        public async Task<NewSubscriptionFormVM> NewSubscriptionPresetForm(NewSubscriptionDto subscriptionBaseDto)
        {
            var subscriptionFormVM = new NewSubscriptionFormVM(RequestService);

            subscriptionFormVM.LastName = subscriptionBaseDto.LastName;
            subscriptionFormVM.FirstName = subscriptionBaseDto.FirstName;
            subscriptionFormVM.BirthDate = subscriptionBaseDto.BirthDate;

            subscriptionFormVM.City = "Erps-Kwerps";
            subscriptionFormVM.PostalCode = "3071";
            subscriptionFormVM.Street = "11 rechtestraat";
            subscriptionFormVM.Phone = "+32483593562";
            subscriptionFormVM.Email = "mickael.thumerel@phoenixhockey.be";

            subscriptionFormVM.SetupForm(Guid.NewGuid(), subscriptionBaseDto.SeasonId);

            return subscriptionFormVM;
        }

        #region Tools

        /// <summary>
        /// Initialize the menu view model
        /// </summary>
        private async Task InitializeCommonViewModelAsync(MultiPartPageViewModel multiPartPageViewModel)
        {
            multiPartPageViewModel.AddViewModel("Menu", new UserMenuViewModel(this.RequestService, await this._menuBL.GetMenuAsync("User")));
            multiPartPageViewModel.AddViewModel<UserInfoViewModel>("UserInfo");
        }

        #endregion

        #endregion
    }
}
