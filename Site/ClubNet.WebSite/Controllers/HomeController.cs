using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClubNet.WebSite.Models;
using ClubNet.WebSite.DataLayer;
using Microsoft.Extensions.Configuration;
using ClubNet.WebSite.Middleware;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using ClubNet.WebSite.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace ClubNet.WebSite.Controllers
{
    [AllowAnonymous]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class HomeController : Controller
    {
        #region Fields

        private readonly IConfigService _configService;
        
        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="HomeController"/>
        /// </summary>
        public HomeController(IConfigService configService) 
        {
            _configService = configService;
        }

        #endregion
        #region Methods

        public IActionResult Index()
        {
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Redirect to the home index in the good language
        /// </summary>
        public IActionResult RedirectToDefaultLanguage()
        {
            var feature = HttpContext.Features.Get<IRequestCultureFeature>();
            var currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();

            return RedirectToAction("Index", new { lang = currentLanguage });
        }


        #endregion
    }
}
