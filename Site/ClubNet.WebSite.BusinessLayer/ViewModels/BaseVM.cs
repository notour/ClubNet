using ClubNet.WebSite.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubNet.WebSite.BusinessLayer.ViewModels
{
    /// <summary>
    /// Base view model 
    /// </summary>
    public abstract class BaseVM
    {
        #region Fields

        private readonly HttpContext _currentContext;
        private string _currentLanguage;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="BaseVM"/>
        /// </summary>
        public BaseVM(HttpContext currentContext)
        {
            _currentContext = currentContext;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current user langugae
        /// </summary>
        protected string CurrentLanguage
        {
            get
            {
                if (string.IsNullOrEmpty(_currentLanguage))
                {
                    var feature = this._currentContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
                }

                return _currentLanguage;
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}
