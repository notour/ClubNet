namespace ClubNet.WebSite.BusinessLayer.Contracts
{
    using System.Threading;
    using System.Threading.Tasks;
    using ClubNet.WebSite.ViewModels.Forms.User;
    using ClubNet.WebSite.ViewModels.User;

    /// <summary>
    /// Define a business layer instance able to managed the user informations
    /// </summary>
    public interface IUserBL
    {
        #region Methods

        /// <summary>
        /// Gets the user subscriptions
        /// </summary>
        Task<UserSubscriptionViewModel> GetUserSubscriptionsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get a new subscription form configured
        /// </summary>
        Task<NewSubscriptionFormVM> GetNewSubscriptionFormVM(CancellationToken timeout);

        #endregion
    }
}
