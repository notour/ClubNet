namespace ClubNet.WebSite
{
    using System;

    using ClubNet.WebSite.Common.Contracts;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Define the global view model of a page
    /// </summary>
    public class PageViewModel<TViewModel> : PageModel
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="PageViewModel"/>
        /// </summary>
        public PageViewModel(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        /// <summary>
        /// Initialize a new instance of the class <see cref="PageViewModel"/>
        /// </summary>
        public PageViewModel(IRequestService requestService)
        {
            ViewModel = CreateViewModel<TViewModel>(requestService);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current view model
        /// </summary>
        public TViewModel ViewModel { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Instanciate the new view model
        /// </summary>
        protected TViewModelType CreateViewModel<TViewModelType>(IRequestService requestService)
        {
            return (TViewModelType)Activator.CreateInstance(typeof(TViewModelType), new object[] { requestService });
        }

        #endregion
    }
}
