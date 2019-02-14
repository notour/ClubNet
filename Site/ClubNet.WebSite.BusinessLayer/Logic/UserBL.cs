namespace ClubNet.WebSite.BusinessLayer.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.DataLayer;
    using ClubNet.WebSite.Domain;
    using ClubNet.WebSite.Domain.Sport;
    using ClubNet.WebSite.Domain.User;
    using ClubNet.WebSite.ViewModels.Forms.User;
    using ClubNet.WebSite.ViewModels.User;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Define the user business layer
    /// </summary>
    sealed class UserBL : BaseBL, IUserBL
    {
        #region Fields

        private readonly IStorageService<Member> _memberStorage;
        private readonly IStorageService<Subscription> _subscriptionStorage;
        private readonly IUserEmailStore<UserInfo> _userEmailService;
        private readonly IStorageService<Season> _seasonStorage;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserBL"/>
        /// </summary>
        public UserBL(IStorageServiceProvider storageServiceProvider, IHttpContextAccessor httpContextAccessor, ISecurityBL securityBL, IConfigService configService, IUserEmailStore<UserInfo> userEmailService)
            : base(httpContextAccessor, securityBL, configService)
        {
            this._memberStorage = storageServiceProvider.GetStorageService<Member>();
            this._seasonStorage = storageServiceProvider.GetStorageService<Season>();
            this._subscriptionStorage = storageServiceProvider.GetStorageService<Subscription>();
            this._userEmailService = userEmailService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the user subscription dashboard
        /// </summary>
        public async Task<UserSubscriptionViewModel> GetUserSubscriptionsAsync(CancellationToken cancellationToken)
        {
            var utcDateNow = DateTime.UtcNow.Date;
            var requestService = RequestService;
            var subscriptionVM = new UserSubscriptionViewModel(requestService);

            var currentSeason = await this._seasonStorage.FindFirstAsync(u => u.EntityType == SportEntityType.Saison && u.End > utcDateNow && u.SubscriptionOpenDate <= utcDateNow, cancellationToken);

            var startHistoryDateSubscription = utcDateNow;

            if (currentSeason != null)
                subscriptionVM.SetupSeasonData(currentSeason);

            var members = await this._memberStorage.FindAllAsync(u => u.Owners.Contains(requestService.UserId), cancellationToken);
            var memberIds = new Guid[0];

            if (members.Any())
            {
                memberIds = members.Select(m => m.Id)
                                   .ToArray();
            }

            var allMembersSubscriptions = await this._subscriptionStorage.FindAllAsync(s => memberIds.Contains(s.MemberId), cancellationToken);

            // TODO : grp subscription by season and members

            return subscriptionVM;
        }

        /// <summary>
        /// Configured a new subscription form
        /// </summary>
        public async Task<NewSubscriptionFormVM> GetNewSubscriptionFormVM(CancellationToken cancellationToken)
        {
            var utcDateNow = DateTime.UtcNow.Date;

            var viewModel = new NewSubscriptionFormVM(RequestService);
            var currentSeason = await this._seasonStorage.FindFirstAsync(u => u.EntityType == SportEntityType.Saison && u.End > utcDateNow && u.SubscriptionOpenDate <= utcDateNow, cancellationToken);

            var ctx = CurrentHttpContext;
            if (ctx.User.HasClaim(c => c.Type == ClaimTypes.Email))
                viewModel.Email = ctx.User.FindFirstValue(ClaimTypes.Email);

            viewModel.SetupForm(null, currentSeason?.Id ?? Guid.Empty);

            return viewModel;
        }

        #endregion
    }
}
