namespace ClubNet.WebSite.Domain.Security
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Group all the criteria need to access the entity
    /// </summary>
    public sealed class SecurityCriteria : Dictionary<SecurityCriteriaType, IEnumerable<Guid>>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="SecurityCriteria"/>
        /// </summary>
        public SecurityCriteria()
        {

        }

        #endregion
    }
}
