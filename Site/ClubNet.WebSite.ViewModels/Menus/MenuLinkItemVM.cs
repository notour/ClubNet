namespace ClubNet.WebSite.ViewModels.Menus
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Domain.Configs.Menus;

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
        public MenuLinkItemVM(MenuLinkItem menuItem, IRequestService requestService)
            : base(menuItem, requestService)
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
