namespace ClubNet.WebSite.Domain.Security
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a security right
    /// </summary>
    [DataContract]
    public sealed class SecurityRight : SecurityEntity<SecurityEntityType>
    {
        #region Ctor

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SecurityRight"/> class.
        ///// </summary>
        //public SecurityRight(Guid id, string name, string description)
        //    : base(id, name, description, SecurityEntityType.Rigth)
        //{
        //}

        #endregion
        public SecurityRight(SecurityEntityType entityType) : base(entityType)
        {
            throw new NotImplementedException();
        }
    }
}
