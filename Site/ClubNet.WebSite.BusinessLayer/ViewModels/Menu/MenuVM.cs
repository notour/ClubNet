using ClubNet.WebSite.Domain.Configs.Menu;

namespace ClubNet.WebSite.ViewModel.Menu
{
    /// <summary>
    /// Define a menu thaht contains children items
    /// </summary>
    public class MenuVM : MenuItemVM
    {
        public MenuVM(MenuItem menuItem) : base(menuItem)
        {
        }
    }
}
