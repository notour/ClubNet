namespace ClubNet.WebSite.BusinessLayer.Logic
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.DataLayer;
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
        public UserBL(IStorageServiceProvider storageServiceProvider,
                      IHttpContextAccessor httpContextAccessor,
                      ISecurityBL securityBL,
                      IConfigService configService,
                      IUserEmailStore<UserInfo> userEmailService)
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
        public async Task<NewSubscriptionFormVM> GetNewSubscriptionFormVMAsync(CancellationToken cancellationToken)
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

        /// <summary>
        /// Save the new subscription. If <paramref name="justSave"/> is false then the subscription is also submit to validation
        /// </summary>
        public async Task SaveNewSubscriptionAsync(NewSubscriptionDto newSubscriptionDto, bool justSave, CancellationToken cancellationToken)
        {
            var memberId = Member.GenerateMemberId(newSubscriptionDto.FirstName,
                                                   newSubscriptionDto.LastName,
                                                   newSubscriptionDto.BirthDate.GetValueOrDefault(),
                                                   string.Empty);

            var memberProfilesThrougtYears = await this._memberStorage.FindAllAsync(m => m.MemberId.StartsWith(memberId), cancellationToken);

            string memberSuffix = string.Empty;
            Member memberProfil = null;

            if (memberProfilesThrougtYears != null && memberProfilesThrougtYears.Any())
            {
                var currentMemberProfilThrougtYears = memberProfilesThrougtYears.Where(m => string.Equals(m.FirstName, newSubscriptionDto.FirstName, StringComparison.OrdinalIgnoreCase) &&
                                                                                            string.Equals(m.LastName, newSubscriptionDto.LastName, StringComparison.OrdinalIgnoreCase) &&
                                                                                            m.BirthDay == newSubscriptionDto.BirthDate);

                if (currentMemberProfilThrougtYears.Any())
                {
                    // Try to found the current profil use
                    memberProfil = currentMemberProfilThrougtYears.SingleOrDefault(c => c.SeasonId == newSubscriptionDto.SeasonId);

                    if (memberProfil == null)
                    {
                        // Take any previous profil to keep the member id
                        var previous = currentMemberProfilThrougtYears.FirstOrDefault();

                        memberSuffix = previous.MemberId.Replace(memberId, "").Trim('-');
                    }
                }
            }

            var contactInfo = ContactInfo.Create(newSubscriptionDto.Email, newSubscriptionDto.Phone, string.Empty);
            var physicialAddress = PhysicalAddress.Create(newSubscriptionDto.City,
                                                          newSubscriptionDto.PostalCode,
                                                          newSubscriptionDto.Street,
                                                          newSubscriptionDto.StreetComplement);

            bool isNew = memberProfil == null;

            if (isNew)
            {
                var userManager = this.CurrentHttpContext.RequestServices.GetService(typeof(UserManager<UserInfo>)) as UserManager<UserInfo>;
                var currentConnectedUser = await userManager.GetUserAsync(this.CurrentHttpContext.User);

                memberProfil = Member.Create(newSubscriptionDto.FirstName,
                                             newSubscriptionDto.LastName,
                                             newSubscriptionDto.BirthDate.GetValueOrDefault(),
                                             newSubscriptionDto.BirthPlace,
                                             (SexeEnum)(int)newSubscriptionDto.Sexe,
                                             contactInfo,
                                             physicialAddress,
                                             null,
                                             null,
                                             new[] { currentConnectedUser },
                                             null,
                                             newSubscriptionDto.SeasonId,
                                             memberSuffix,
                                             justSave);

                await this._memberStorage.CreateAsync(memberProfil, m => m.MemberId == memberProfil.MemberId && m.SeasonId == memberProfil.SeasonId, cancellationToken);
            }
            else
            {
                memberProfil.Update(contactInfo,
                                    physicialAddress,
                                    null,
                                    null,
                                    null,
                                    justSave);

                await this._memberStorage.SaveAsync(memberProfil, cancellationToken);
            }

        }

        #endregion
    }
}
