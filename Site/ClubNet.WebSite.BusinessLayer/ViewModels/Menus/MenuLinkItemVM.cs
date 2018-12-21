using ClubNet.WebSite.Domain.Configs.Menu;

namespace ClubNet.WebSite.ViewModel.Menus
{
    /// <summary>
    /// Define the view model of an item part of a menu
    /// </summary>
    public class MenuLinkItemVM : MenuItemVM
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MenuLinkItemVM"/>
        /// </summary>
        /// <param name="menuItem"></param>
        internal MenuLinkItemVM(LinkedMenuItem menuItem)
            : base(menuItem)
        {

        }

        #endregion

        #region Properties


        #endregion
    }
}
