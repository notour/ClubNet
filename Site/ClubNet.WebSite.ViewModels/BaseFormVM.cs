namespace ClubNet.WebSite.ViewModels
{

    using ClubNet.WebSite.Common.Contracts;

    /// <summary>
    /// Base view model class for all the form instances
    /// </summary>
    public abstract class BaseFormVM : BaseVM
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="BaseFormVM"/>
        /// </summary>
        public BaseFormVM(IRequestService requestService) 
            : base(requestService)
        {
        }

        #endregion
    }
}
