namespace ClubNet.WebSite.BusinessLayer.Logic
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Domain;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Define the business layer available to managed the security
    /// </summary>
    class SecurityBL : ISecurityBL
    {
        #region Methods

        /// <summary>
        /// Filter the allowed entities in function of the security user
        /// </summary>
        public Task<IEnumerable<ISecurityEntity>> FilterEntityAsync(IEnumerable<ISecurityEntity> entities, IRequestService requestService)
        {
            return Task<IEnumerable<ISecurityEntity>>.FromResult(entities);
        }

        #endregion
    }
}
