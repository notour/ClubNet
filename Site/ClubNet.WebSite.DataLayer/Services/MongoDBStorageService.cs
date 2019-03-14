namespace ClubNet.WebSite.DataLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using ClubNet.Framework.Memory;
    using ClubNet.WebSite.Common.Exceptions;
    using ClubNet.WebSite.Domain;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;

    /// <summary>
    /// Storage service dedicated to the mongo db DataBases
    /// </summary>
    internal class MongoDBStorageService<TEntity> : Disposable, IStorageService<TEntity>
            where TEntity : IEntity
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
                TEntity exist = await OnFind(unicitySelector).Limit(1).FirstOrDefaultAsync(cancellationToken);
                if (EqualityComparer<TEntity>.Equals(exist, EqualityComparer<TEntity>.Default))
                {
                    if (thownOnConflict)
                        throw new NotImplementedException("Exception type and management not implemented yet in case of conflict");
                    return exist;
                }
            }
            entity.ConcurrencyStamp = Guid.NewGuid().ToString();
            this._mongoDBCollection.InsertOne(entity, null, cancellationToken);

            return entity;
        }

        /// <summary>
        /// Save the current document
        /// </summary>
        public async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            string localConcurrencyStamp = entity.ConcurrencyStamp;
            entity.ConcurrencyStamp = Guid.NewGuid().ToString();
            ReplaceOneResult saveResult = await this._mongoDBCollection.ReplaceOneAsync(u => u.Id == entity.Id && u.ConcurrencyStamp == localConcurrencyStamp, entity, cancellationToken: cancellationToken);

            if (saveResult.IsModifiedCountAvailable && saveResult.ModifiedCount == 0)
            {
                entity.ConcurrencyStamp = localConcurrencyStamp;
                throw new ConcurrencySaveException();
            }

            return entity;
        }

        /// <summary>
        /// Update only one field
        /// </summary>
        public async Task<TEntity> UpdateFieldAsync<TFieldValue>(TEntity entity, Expression<Func<TEntity, TFieldValue>> field, TFieldValue newValue, CancellationToken cancellationToken)
        {
            TEntity result = await this._mongoDBCollection.FindOneAndUpdateAsync(u => u.Id == entity.Id && u.ConcurrencyStamp == entity.ConcurrencyStamp,
                                                                             Builders<TEntity>.Update.Combine(
                                                                             Builders<TEntity>.Update.Set(field, newValue),
                                                                             Builders<TEntity>.Update.Set(u => u.ConcurrencyStamp, Guid.NewGuid().ToString())),
                                                                             cancellationToken: cancellationToken);

            if (result == null)
                throw new ConcurrencySaveException();

            return result;
        }

        #endregion

        #region Tools

        /// <summary>
        /// Search in the mongo db collection
        /// </summary>
        protected virtual IFindFluent<TEntity, TProjection> OnFind<TProjection>(Expression<Func<TEntity, bool>> filter)
        {
            ProjectionDefinition<TEntity, TProjection> projection = GetProjection<TProjection>();
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
                if (s_projections.TryGetValue(key, out BsonDocument projection))
                    return projection;
            }

            using (s_projectionLocker.LockWrite())
            {
                if (s_projections.TryGetValue(key, out BsonDocument projection))
                    return projection;

                ProjectionDefinition<TEntity, TProjection> newProjection = Builders<TEntity>.Projection.As<TProjection>();
                s_projections = s_projections.Add(key, newProjection.Render(this._mongoDBCollection.DocumentSerializer, BsonSerializer.SerializerRegistry).Document);

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
