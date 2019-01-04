namespace ClubNet.WebSite.Tools
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ClubNet.WebSite.Domain.User;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extend the current <see cref="ClaimsIdentity"/> to add custom informations
    /// </summary>
    public class UserInfoClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserInfo>, IUserClaimsPrincipalFactory<UserInfo>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserInfoClaimsPrincipalFactory"/>
        /// </summary>
        public UserInfoClaimsPrincipalFactory(UserManager<UserInfo> userManager, IOptions<IdentityOptions> optionsAccessor) 
            : base(userManager, optionsAccessor)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populate the current principal claims with the user data
        /// </summary>
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserInfo user)
        {
            var principal = await base.GenerateClaimsAsync(user);

            principal.AddClaim(new Claim(nameof(UserInfo.PreferredCulture), user.PreferredCulture.TwoLetterISOLanguageName));

            return principal;
        }

        #endregion

    }
}
