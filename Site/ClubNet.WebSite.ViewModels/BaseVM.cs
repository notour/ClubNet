﻿namespace ClubNet.WebSite.ViewModels
{
    using ClubNet.WebSite.BusinessLayer.Contracts;

    /// <summary>
    /// Base view model 
    /// </summary>
    public abstract class BaseVM
    {
        #region Fields

        private readonly IRequestService _requestService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="BaseVM"/>
        /// </summary>
        public BaseVM(IRequestService requestService)
        {
            _requestService = requestService;
            Errors = new ErrorViewModel(requestService);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current user langugae
        /// </summary>
        protected string CurrentLanguage
        {
            get { return _requestService.CurrentLanguage.TwoLetterISOLanguageName; }
        }

        /// <summary>
        /// Gets the error informations
        /// </summary>
        public ErrorViewModel Errors { get; }

        #endregion

        #region Methods

        #endregion
    }
}