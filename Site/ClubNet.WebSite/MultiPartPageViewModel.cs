namespace ClubNet.WebSite
{
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.ViewModels;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Define the global view model of a page
    /// </summary>
    public sealed class MultiPartPageViewModel : PageViewModel<IReadOnlyDictionary<string, BaseVM>>
    {
        #region Fields

        private readonly IDictionary<string, BaseVM> _sortedViewModels;
        private readonly IRequestService _requestService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="PageViewModel"/>
        /// </summary>
        public MultiPartPageViewModel(IRequestService requestService)
            : base(new SortedDictionary<string, BaseVM>())
        {
            this._requestService = requestService;
            this._sortedViewModels = (IDictionary<string, BaseVM>)ViewModel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert a new view model instance
        /// </summary>
        public void AddViewModel<TViewModel>(string viewName)
            where TViewModel : BaseVM
        {
            this.AddViewModel(viewName, CreateViewModel<TViewModel>(this._requestService));
        }

        /// <summary>
        /// Insert a new view model instance
        /// </summary>
        public void AddViewModel<TViewModel>(string viewName, TViewModel instance)
            where TViewModel : BaseVM
        {
            this._sortedViewModels.Add(viewName, instance);
        }

        /// <summary>
        /// Insert a new view model instance
        /// </summary>
        public void AddViewModel<TViewModel>(TViewModel instance)
            where TViewModel : BaseVM
        {
            this._sortedViewModels.Add(typeof(TViewModel).Name, instance);
        }

        /// <summary>
        /// Get the view Model typed
        /// </summary>
        public TViewModel Get<TViewModel>(string viewName)
            where TViewModel : BaseVM
        {
            return (TViewModel)this._sortedViewModels[viewName];
        }

        #endregion
    }
}
