namespace ClubNet.WebSite.Domain.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define the relation between the group and the users
    /// </summary>
    [DataContract]
    public sealed class SecurityUser : Entity<SecurityEntityType>
    {

        //#region Ctor

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SecurityUser"/> class.
        ///// </summary>
        //public SecurityUser(Guid id, IEnumerable<Guid> groupIds)
        //    : base(id, SecurityEntityType.User)
        //{
        //    GroupIds = groupIds;
        //}

        //#endregion

        //#region Properties

        ///// <summary>
        ///// Gets the group ids.
        ///// </summary>
        //[DataMember]
        //public IEnumerable<Guid> GroupIds { get; }

        //#endregion
        public SecurityUser(SecurityEntityType entityType) : base(entityType)
        {
            throw new NotImplementedException();
        }
    }
}
