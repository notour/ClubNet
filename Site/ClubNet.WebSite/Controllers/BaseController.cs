namespace ClubNet.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Middleware;
    using ClubNet.WebSite.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// Base custom class of all controllers
    /// </summary>
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public abstract class BaseController : Controller
    {
        #region Fields

        private const string MESSAGES = "Messages";
        private readonly IHttpContextAccessor _contextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="BaseController"/>
        /// </summary>
        public BaseController(IServiceProvider serviceProvider, ILogger logger, IResourceService resourceService)
        {
            this.ResourceService = resourceService;
            this.Localizer = (IStringLocalizer)serviceProvider.GetService(typeof(IStringLocalizer<>).MakeGenericType(GetType()));
            this._contextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            this.Logger = logger;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets the current request Service
        /// </summary>
        protected IRequestService RequestService
        {
            get { return this._contextAccessor.CurrentRequestService(); }
        }

        /// <summary>
        /// Gets the logger instance
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the current resource service
        /// </summary>
        public IResourceService ResourceService { get; }

        /// <summary>
        /// Gets the local localizer
        /// </summary>
        public IStringLocalizer Localizer { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Add a message to the current view data
        /// </summary>
        protected void AddMessage(string code, MessageType messageType, params string[] arguments)
        {
            var message = this.Localizer.GetString(code, arguments);

            if (!this.ViewData.ContainsKey(MESSAGES))
                this.ViewData[MESSAGES] = new List<MessageViewModel>();

            var messages = (List<MessageViewModel>)this.ViewData[MESSAGES];
            messages.Add(new MessageViewModel(message, messageType));
        }

        /// <summary>
        /// Save messages before redirection
        /// </summary>
        protected void SaveMessages()
        {
            if (this.ViewData.ContainsKey(MESSAGES))
                this.TempData[MESSAGES] = JsonConvert.SerializeObject(this.ViewData[MESSAGES]);
        }

        /// <summary>
        /// Save the current model state before redirection
        /// </summary>
        protected void SaveModelState()
        {
            if (this.ModelState.Keys.Any())
                this.TempData[nameof(ModelState)] = this.ModelState.Save();
        }

        /// <summary>
        /// Managed view info from ViewData/ViewBag and TempData
        /// </summary>
        protected void InitializeCurrentViewInfo()
        {
            if (this.TempData.ContainsKey(MESSAGES) && this.TempData[MESSAGES] is string messages)
            {
                this.ViewData[MESSAGES] = JsonConvert.DeserializeObject<List<MessageViewModel>>(messages);
                this.TempData.Remove(MESSAGES);
            }

            if (this.TempData.TryGetValue(nameof(ModelState), out var tmpModelState) && tmpModelState is string modelStateJson)
            {
                this.TempData.Remove(nameof(ModelState));
                this.ModelState.Restore(modelStateJson, this.Logger);
            }

            this.TempData.Clear();
            this.TempData.Save();
        }

        /// <summary>
        /// Redirect to the specific url or to the  index
        /// </summary>
        protected IActionResult LocalizedRedirect(string returnUrl, CultureInfo lang = null)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return base.Redirect(ChangeUrlLanguage(returnUrl, lang));

            return RedirectToAction(nameof(HomeController.Index), "Home", new { lang = lang ?? RequestService.CurrentLanguage });
        }

        /// <summary>
        /// Change the url language
        /// </summary>
        private string ChangeUrlLanguage(string url, CultureInfo culture)
        {
            if (Regex.IsMatch(url, "^(/[a-zA-Z]{2}/)"))
                url = url.Substring(4);
            return ($"/{culture}/" + url).Replace("//", "/");
        }

        #endregion
    }
}
