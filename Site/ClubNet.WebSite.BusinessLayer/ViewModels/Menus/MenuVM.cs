using ClubNet.WebSite.Domain.Configs.Menu;
using System.Collections.Generic;

namespace ClubNet.WebSite.ViewModel.Menus
{
    /// <summary>
    /// Define a menu thaht contains children items
    /// </summary>
    public class MenuVM : MenuItemVM
    {
        #region Fields

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        internal MenuVM(Menu menu, IEnumerable<MenuItemVM> items) 
            : base(menu)
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
