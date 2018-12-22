namespace ClubNet.WebSite.Domain.Security
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a security group that define the specific rigth allowed for the entity of this group
    /// </summary>
    [DataContract]
    public sealed class SecurityGroup : SecurityEntity<SecurityEntityType>
    {
        //#region Ctor

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SecurityGroup"/> class.
        ///// </summary>
        //public SecurityGroup(Guid id, string name, string description, IEnumerable<Guid> rightAllowed)
        //    : base(id, name, description, SecurityEntityType.Group)
        //{
        //    RightAllowed = rightAllowed;
        //}

        //#endregion

        //#region Properties

        ///// <summary>
        ///// Gets the right allowed by this groups
        ///// </summary>
        //[DataMember]
        //public IEnumerable<Guid> RightAllowed { get; }

        //#endregion
        public SecurityGroup(SecurityEntityType entityType) : base(entityType)
        {
            throw new NotImplementedException();
        }
    }
}
