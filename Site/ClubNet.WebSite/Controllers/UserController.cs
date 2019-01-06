namespace ClubNet.WebSite.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.ViewModels;
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

        private readonly IMenuBL _menuBL;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserController"/>
        /// </summary>
        public UserController(IServiceProvider serviceProvider, ILogger<UserController> logger, IResourceService resourceService, IMenuBL menuBL) 
            : base(serviceProvider, logger, resourceService)
        {
            this._menuBL = menuBL;
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
            var pageVm = new MultiPartPageViewModel(this.RequestService);

            await InitializeCommonViewModelAsync(pageVm);

            pageVm.AddViewModel<UserSubscriptionViewModel>("UserSubscriptions");

            return View("_UserLayout", pageVm);
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
