namespace ClubNet.WebSite.ViewModels.Forms.User
{
    using System;
    using ClubNet.WebSite.Common.Contracts;

    /// <summary>
    /// New subscription form
    /// </summary>
    public sealed class NewSubscriptionFormVM : BaseFormVM
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="NewSubscriptionFormVM"/>
        /// </summary>
        /// <param name="requestService"></param>
        public NewSubscriptionFormVM(IRequestService requestService) 
            : base(requestService)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the member id target by this current subscription
        /// </summary>
        public Guid? MemberId { get; private set; }

        /// <summary>
        /// Gets the current season associate to the current subscription
        /// </summary>
        public Guid? SeasonId { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Setup the current view model
        /// </summary>
        public void SetupForm(Guid? memberId, Guid? seasonId)
        {
            this.MemberId = memberId;
            this.SeasonId = seasonId;
        }

        #endregion
    }
}
