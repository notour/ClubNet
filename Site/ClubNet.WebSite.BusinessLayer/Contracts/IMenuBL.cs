using ClubNet.WebSite.ViewModel.Menus;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubNet.WebSite.BusinessLayer.Contracts
{
    /// <summary>
    /// Define the business layer instance responsible of the menu managments
    /// </summary>
    public interface IMenuBL
    {
        /// <summary>
        /// Get the menu name view model
        /// </summary>
        Task<IEnumerable<MenuItemVM>> GetMenuAsync(string menuName);
    }
}
