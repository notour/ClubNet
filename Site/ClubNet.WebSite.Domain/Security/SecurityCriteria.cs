using System;
using System.Collections.Generic;
using System.Text;

namespace ClubNet.WebSite.Domain.Security
{
    /// <summary>
    /// Group all the criteria need to access the entity
    /// </summary>
    public sealed class SecurityCriteria : Dictionary<SecurityCriteriaType, IEnumerable<Guid>>
    {
    }
}
