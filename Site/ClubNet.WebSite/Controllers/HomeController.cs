namespace ClubNet.WebSite.Controllers
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;

    /// <summary>
    /// Controller of the home pages
    /// </summary>
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        #region Fields

        private readonly IConfigService _configService;
        
        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="HomeController"/>
        /// </summary>
        public HomeController(IConfigService configService, IServiceProvider serviceProvider, ILogger<HomeController> logger, IResourceService resourceService) 
            : base(serviceProvider, logger, resourceService)
        {
            _configService = configService;
        }

        #endregion
        #region Methods

        public IActionResult Index()
        {
            AddMessage("Test", MessageType.Success);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(IRequestService requestService)
        {
            return View(new ErrorViewModel(requestService));
        }

        /// <summary>
        /// Redirect to the home index in the good language
        /// </summary>
        public IActionResult RedirectToDefaultLanguage(IRequestService requestService)
        {
            return RedirectToAction("Index", new { lang = requestService.CurrentLanguage });
        }


        #endregion
    }
}
