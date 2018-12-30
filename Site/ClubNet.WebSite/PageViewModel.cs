namespace ClubNet.WebSite
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.ViewModels;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using System;

    /// <summary>
    /// Define the global view model of a page
    /// </summary>
    public class PageViewModel<TViewModel> : PageModel
        where TViewModel : BaseVM
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
            ViewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel), new object[] { requestService });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current view model
        /// </summary>
        public TViewModel ViewModel { get; }

        #endregion
    }
}
