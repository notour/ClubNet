using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using ClubNet.WebSite.ViewModel;

namespace ClubNet.WebSite.Managers.Implementations
{
    /// <summary>
    /// Managed the menu display & configuration in function of the user informations
    /// </summary>
    sealed class MenuManager : IMenuManager
    {
        #region Fields

        private readonly ImmutableDictionary<string, MenuVM> s_menuCache;
        private readonly IServiceProvider ServiceProvider;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MenuManager"/>
        /// </summary>
        public MenuManager(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        #endregion

        #region Methods

        public IEnumerable<MenuItemVM> GetMenu(string menuId)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
