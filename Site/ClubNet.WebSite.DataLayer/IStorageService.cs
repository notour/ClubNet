namespace ClubNet.WebSite.DataLayer
{
    /// <summary>
    /// Define a storage service associate to the type <typeparamref name="TEntity"/>
    /// </summary>
    public interface IStorageService
    {
    }

    /// <summary>
    /// Define a storage service associate to the type <typeparamref name="TEntity"/>
    /// </summary>
    public interface IStorageService<TEntity> : IStorageService
    {
    }
}
