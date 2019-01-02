namespace ClubNet.WebSite.Controllers
{
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Middleware;
    using ClubNet.WebSite.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base custom class of all controllers
    /// </summary>
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public abstract class BaseController : Controller
    {
        #region Fields

        private const string Messages = "Messages";
        private readonly IHttpContextAccessor _contextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="BaseController"/>
        /// </summary>
        public BaseController(IServiceProvider serviceProvider, ILogger logger, IResourceService resourceService)
        {
            ResourceService = resourceService;
            Localizer = (IStringLocalizer)serviceProvider.GetService(typeof(IStringLocalizer<>).MakeGenericType(this.GetType()));
            _contextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            Logger = logger;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets the current request Service
        /// </summary>
        protected IRequestService RequestService
        {
            get { return _contextAccessor.CurrentRequestService(); }
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
            var message = Localizer.GetString(code, arguments);

            if (!ViewData.ContainsKey(Messages))
                ViewData[Messages] = new List<MessageViewModel>();

            var messages = (List<MessageViewModel>)ViewData[Messages];
            messages.Add(new MessageViewModel(message, messageType));
        }

        /// <summary>
        /// Pass message to next view
        /// </summary>
        protected void PassMessages()
        {
            if (ViewData.ContainsKey(Messages))
                TempData[Messages] = JsonConvert.SerializeObject(ViewData[Messages]);
        }

        /// <summary>
        /// Managed view info from ViewData/ViewBag and TempData
        /// </summary>
        protected void InitializeCurrentViewInfo()
        {
            if (TempData.ContainsKey(Messages) && TempData[Messages] is string messages)
            {
                ViewData[Messages] = JsonConvert.DeserializeObject<List<MessageViewModel>>(messages);
                TempData.Remove(Messages);
            }
        }

        #endregion
    }
}
