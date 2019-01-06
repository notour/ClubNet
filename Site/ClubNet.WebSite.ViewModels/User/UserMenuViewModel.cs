namespace ClubNet.WebSite.ViewModels.User
{
    using System.Collections.Generic;

    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.ViewModels.Menus;

    /// <summary>
    /// Define a view model for the menu view in the user profile
    /// </summary>
    public sealed class UserMenuViewModel : BaseVM
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserMenuViewModel"/>
        /// </summary>
        public UserMenuViewModel(IRequestService requestService, IEnumerable<MenuItemVM> menuItemsVM)
            : base(requestService)
        {
            this.MenuItems = menuItemsVM;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu collection
        /// </summary>
        public IEnumerable<MenuItemVM> MenuItems { get; }

        #endregion
    }
}
