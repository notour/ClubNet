namespace ClubNet.WebSite.BusinessLayer.Contracts
{
    using ClubNet.WebSite.ViewModels.Menus;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
