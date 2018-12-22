namespace ClubNet.WebSite.ViewModel.Menus
{
    using ClubNet.WebSite.BusinessLayer.ViewModels;
    using ClubNet.WebSite.Domain.Configs.Menus;
    using Microsoft.AspNetCore.Http;
    using System;

    /// <summary>
    /// Define the view model of an item part of a menu
    /// </summary>
    public abstract class MenuItemVM : BaseVM
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MenuItemVM"/>
        /// </summary>
        /// <param name="menuItem"></param>
        protected MenuItemVM(MenuItem menuItem, HttpContext httpContext)
            : base(httpContext)
        {
            Label = menuItem.Label?.GetLocalizedValue(CurrentLanguage);
            Name = menuItem.Name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the menu label name
        /// </summary>
        public string Label { get; }


        #endregion
    }
}
