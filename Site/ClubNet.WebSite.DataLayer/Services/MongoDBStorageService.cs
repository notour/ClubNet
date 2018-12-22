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
        /// Initialize the class <see cref="MongoDBStorageService{TEntity}"/>
        /// </summary>
        static MongoDBStorageService()
        {
            s_projectionLocker = new ReaderWriterLockSlim();
        }

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

        #region Find

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

        #endregion

        #region Save/Update

        /// <summary>
        /// Create a new entity in the system
        /// </summary>
        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return CreateAsync(entity, null, cancellationToken);
        }

        /// <summary>
        /// Create a new item it an existing one exist with the unicity selector then return it except if the parameter thownOnConflict is set to true
        /// </summary>
        public async Task<TEntity> CreateAsync(TEntity entity, Expression<Func<TEntity, bool>> unicitySelector, CancellationToken cancellationToken, bool thownOnConflict = false)
        {
            if (unicitySelector != null)
            {
                var exist = await OnFind(unicitySelector).Limit(1).FirstOrDefaultAsync(cancellationToken);
                if (EqualityComparer<TEntity>.Equals(exist, EqualityComparer<TEntity>.Default))
                {
                    if (thownOnConflict)
                        throw new NotImplementedException("Exception type and management not implemented yet in case of conflict");
                    return exist;
                }
            }
            this._mongoDBCollection.InsertOne(entity, null, cancellationToken);

            return entity;
        }

        public Task<TEntity> SaveAsync(TEntity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

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
