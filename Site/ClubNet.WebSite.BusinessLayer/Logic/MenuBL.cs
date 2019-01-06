namespace ClubNet.WebSite.BusinessLayer.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ClubNet.Framework.Helpers;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.DataLayer;
    using ClubNet.WebSite.Domain;
    using ClubNet.WebSite.Domain.Configs.Menus;
    using ClubNet.WebSite.ViewModels.Menus;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Business layer implementation of the contract <see cref="IMenuBL"/> in charge of the club net menu managment
    /// </summary>
    internal class MenuBL : BaseBL, IMenuBL
    {
        #region Fields

        private readonly IStorageService<Menu> _serviceProvider;
        private readonly ISecurityBL _securityBL;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="MenuBL"/>
        /// </summary>
        static MenuBL()
        {
        }

        /// <summary>
        /// Initialize a new instance of the class <see cref="MenuBL"/>
        /// </summary>
        public MenuBL(IHttpContextAccessor contextAccessor, IConfigService configService, IStorageServiceProvider serviceProvider, ISecurityBL securityBL)
            : base(contextAccessor, securityBL, configService)
        {
            this._serviceProvider = serviceProvider.GetStorageService<Menu>();
            this._securityBL = securityBL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the menu view models by name
        /// </summary>
        public async Task<IEnumerable<MenuItemVM>> GetMenuAsync(string menuName)
        {
            try
            {
                var menu = await this._serviceProvider.FindFirstAsync(m => m.EntityType == Domain.Configs.ConfigType.Menu && m.Name == menuName, this.RequestService.CancellationToken);

                if (menu == null)
                    return EnumerableHelper<MenuItemVM>.Empty;

                var allowedItems = await this._securityBL.FilterEntityAsync(ExtractMenuItems(menu), this.RequestService);

                var menuVM = FormatMenu(menu, allowedItems.Select(i => i.Id).ToHashSet()) as MenuVM;
                return menuVM.Items;
            }
            catch (OperationCanceledException)
            {
                // TODO : Log error
                return EnumerableHelper<MenuItemVM>.Empty;
            }
        }

        /// <summary>
        /// Generate view models from menu
        /// </summary>
        private MenuItemVM FormatMenu(MenuItem menuItem, HashSet<Guid> allowedItems)
        {
            if (menuItem == null || !allowedItems.Contains(menuItem.Id))
                return null;

            if (menuItem is Menu menu)
            {
                var childrens = menu.Items?.Select(i => FormatMenu(i, allowedItems))
                                           .Where(vm => vm != null)
                                           .ToArray();
                return new MenuVM(menu, childrens, this.RequestService);
            }

            if (menuItem is MenuLinkItem link)
                return new MenuLinkItemVM(link, this.RequestService);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Navigate through the menu to extract all the menu items
        /// </summary>
        private IEnumerable<ISecurityEntity> ExtractMenuItems(MenuItem menuItem)
        {
            yield return menuItem;

            if (menuItem is Menu menu)
            {
                foreach (var item in menu.Items)
                {
                    var items = ExtractMenuItems(item);
                    foreach (var subItem in items)
                        yield return subItem;
                }
            }
        }

        #endregion
    }
}
