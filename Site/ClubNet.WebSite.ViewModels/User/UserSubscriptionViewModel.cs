namespace ClubNet.WebSite.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Domain.Sport;

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

        #region Properties

        /// <summary>
        /// Gets a value indicating if the current subscription on the season is opened
        /// </summary>
        public bool SeasonSubscriptionOpened { get; private set; }

        /// <summary>
        /// Gets a value indicating the current season name
        /// </summary>
        public string SeasonName { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Setup the current season data
        /// </summary>
        public void SetupSeasonData(Season season)
        {
            SeasonName = season.Start.Year + "/" + season.End.Year;
            SeasonSubscriptionOpened = DateTime.Now >= season.SubscriptionOpenDate && DateTime.Now <= season.End;
        }

        #endregion
    }
}
