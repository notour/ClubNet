namespace ClubNet.WebSite.ViewModels.Menus
{
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Domain.Configs.Menus;

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
        protected MenuItemVM(MenuItem menuItem, IRequestService requestService)
            : base(requestService)
        {
            Label = menuItem.Label?.GetLocalizedValue(CurrentLanguage);
            Name = menuItem.Name;
            Glyphicon = menuItem.Glyphicon;
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

        /// <summary>
        /// Gets the menu glyphicon
        /// </summary>
        public string Glyphicon { get; }

        #endregion
    }
}
