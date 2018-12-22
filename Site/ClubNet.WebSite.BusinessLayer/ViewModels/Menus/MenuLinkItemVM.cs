namespace ClubNet.WebSite.ViewModel.Menus
{
    using ClubNet.WebSite.Domain.Configs.Menus;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Define the view model of an item part of a menu
    /// </summary>
    public class MenuLinkItemVM : MenuItemVM
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MenuLinkItemVM"/>
        /// </summary>
        /// <param name="menuItem"></param>
        internal MenuLinkItemVM(MenuLinkItem menuItem, HttpContext httpContext)
            : base(menuItem, httpContext)
        {
            Controller = menuItem.Controller;
            Action = menuItem.Action;
            Url = menuItem.Url;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the controller
        /// </summary>
        public string Controller { get; }

        /// <summary>
        /// Gets the action to call in the controller
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Gets the direct url
        /// </summary>
        public string Url { get; }


        #endregion
    }
}
