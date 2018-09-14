namespace ClubNet.WebSite.DataLayer
{
    /// <summary>
    /// Define a provider in charge of managing the storage service dedicated to the specific entity
    /// </summary>
    public interface IStorageServiceProvider
    {
        #region Methods

        /// <summary>
        /// Gets the specific storage for the specific <typeparamref name="TEntity"/>
        /// </summary>
        IStorageService<TEntity> GetStorageService<TEntity>();

        #endregion
    }
}
