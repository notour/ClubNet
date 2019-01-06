namespace ClubNet.WebSite.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ClubNet.WebSite.Common.Contracts;

    /// <summary>
    /// Define a view model for all the subscriptions
    /// </summary>
    public sealed class UserSubscriptionViewModel : BaseVM
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserSubscriptionViewModel"/>
        /// </summary>
        /// <param name="requestService"></param>
        public UserSubscriptionViewModel(IRequestService requestService) 
            : base(requestService)
        {
        }
     
        #endregion
    }
}
