namespace ClubNet.WebSite.ViewModel.Menus
{
    using ClubNet.WebSite.Domain.Configs.Menus;
    using Microsoft.AspNetCore.Http;
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
        internal MenuVM(Menu menu, IEnumerable<MenuItemVM> items, HttpContext httpContext) 
            : base(menu, httpContext)
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
