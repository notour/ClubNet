using System;
namespace ClubNet.WebSite.Domain.Security
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class for all the security entity
    /// </summary>
    [DataContract]
    public abstract class SecurityEntity<TEntityType> : Entity<TEntityType>
    {
        //#region Ctor

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SecurityEntity{TEntityType}"/> class.
        ///// </summary>
        //protected SecurityEntity(Guid id, string name, string description, TEntityType entityType)
        //    : base(id, entityType)
        //{
        //}

        //#endregion

        //#region Properties

        ///// <summary>
        ///// Gets the name 
        ///// </summary>
        //[DataMember]
        //public string Name { get; }

        ///// <summary>
        ///// Gets the description
        ///// </summary>
        //[DataMember]
        //public string Description { get; }

        //#endregion
        protected SecurityEntity(TEntityType entityType) : base(entityType)
        {
            throw new NotImplementedException();
        }
    }
}
