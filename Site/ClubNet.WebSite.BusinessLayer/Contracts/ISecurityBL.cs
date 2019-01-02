namespace ClubNet.WebSite.BusinessLayer.Contracts
{
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Domain;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Define the security business
    /// </summary>
    public interface ISecurityBL
    {
        /// <summary>
        /// Filter the allowed entities
        /// </summary>
        Task<IEnumerable<ISecurityEntity>> FilterEntityAsync(IEnumerable<ISecurityEntity> entities, IRequestService requestService);
    }
}
