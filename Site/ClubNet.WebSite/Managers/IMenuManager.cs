using System.Collections.Generic;

using ClubNet.WebSite.ViewModel;

namespace ClubNet.WebSite.Managers
{
    /// <summary>
    /// Define a manager in charge of all the menu informations
    /// </summary>
    public interface IMenuManager
    {
        #region Methods

        /// <summary>
        /// Gets the menu view models
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        IEnumerable<MenuItemVM> GetMenu(string menuId);

        #endregion
    }
}
