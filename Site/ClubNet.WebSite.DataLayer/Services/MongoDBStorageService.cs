using MongoDB.Driver;

namespace ClubNet.WebSite.DataLayer.Services
{
    /// <summary>
    /// Storage service dedicated to the mongo db DataBases
    /// </summary>
    class MongoDBStorageService<TEntity> : IStorageService<TEntity>
    {
        #region Fields

        private readonly IMongoCollection<TEntity> _mongoDBCollection;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MongoDBStorageService"/>
        /// </summary>
        /// <param name="mongoDBCollection"></param>
        public MongoDBStorageService(IMongoCollection<TEntity> mongoDBCollection)
        {
            this._mongoDBCollection = mongoDBCollection;
        }

        #endregion

        #region Methods



        #endregion
    }
}
