using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ClubNet.Framework.Memory;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ClubNet.WebSite.DataLayer.Services
{
    /// <summary>
    /// Storage service dedicated to the mongo db DataBases
    /// </summary>
    class MongoDBStorageService<TEntity> : Disposable, IStorageService<TEntity>
    {
        #region Fields

        private static readonly ReaderWriterLockSlim s_projectionLocker;
        private static ImmutableDictionary<Type, BsonDocument> s_projections;

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

        /// <summary>
        /// Find the first result that match the filter
        /// </summary>
        public Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            return OnFind(filter).Limit(1).SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Find the first result that match the filter
        /// </summary>
        public Task<TProjection> FindFirstAsync<TProjection>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            return OnFind<TProjection>(filter).Limit(1).SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Find all the occurence that math the expression
        /// </summary>
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            return await OnFind(filter).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Find all the occurence that math the expression
        /// </summary>
        public async Task<IEnumerable<TProjection>> FindAllAsync<TProjection>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            return await OnFind<TProjection>(filter).ToListAsync(cancellationToken);
        }

        #region Tools

        /// <summary>
        /// Search in the mongo db collection
        /// </summary>
        protected virtual IFindFluent<TEntity, TProjection> OnFind<TProjection>(Expression<Func<TEntity, bool>> filter)
        {
            var projection = GetProjection<TProjection>();
            return this._mongoDBCollection.Find(filter).Project<TProjection>(projection);
        }

        /// <summary>
        /// Build or get the projection definitions
        /// </summary>
        private ProjectionDefinition<TEntity, TProjection> GetProjection<TProjection>()
        {
            var key = typeof(TEntity);
            using (s_projectionLocker.LockRead())
            {
                if (s_projections.TryGetValue(key, out var projection))
                    return projection;
            }

            using (s_projectionLocker.LockWrite())
            {
                if (s_projections.TryGetValue(key, out var projection))
                    return projection;

                var newProjection = Builders<TEntity>.Projection.As<TProjection>();
                s_projections = s_projections.Add(key, newProjection.Render(_mongoDBCollection.DocumentSerializer, BsonSerializer.SerializerRegistry).Document);

                return newProjection;
            }
        }

        /// <summary>
        /// Search in the mongo db collection
        /// </summary>
        protected virtual IFindFluent<TEntity, TEntity> OnFind(Expression<Func<TEntity, bool>> filter)
        {
            return this._mongoDBCollection.Find(filter);
        }

        #endregion

        #endregion
    }
}
