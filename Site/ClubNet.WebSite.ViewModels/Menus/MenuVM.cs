namespace ClubNet.WebSite.ViewModels.Menus
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Domain.Configs.Menus;

    using System.Collections.Generic;

    /// <summary>
    /// Define a menu thaht contains children items
    /// </summary>
    public class MenuVM : MenuItemVM
    {
        #region Fields

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MenuVM"/>
        /// </summary>
        public MenuVM(Menu menu, IEnumerable<MenuItemVM> items, IRequestService requestService) 
            : base(menu, requestService)
        {
            this.Items = items;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the childrent items
        /// </summary>
        public IEnumerable<MenuItemVM> Items { get; }

        #endregion
    }
}
