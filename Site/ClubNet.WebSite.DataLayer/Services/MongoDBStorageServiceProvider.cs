namespace ClubNet.WebSite.DataLayer.Services
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading;

    using ClubNet.WebSite.DataLayer.Configurations;
    using ClubNet.WebSite.Domain;

    using Microsoft.Extensions.Options;

    using MongoDB.Driver;

    /// <summary>
    /// Storage provider that connect configuration and <see cref="MongoDBStorageService{TEntity}"/> specific 
    /// </summary>
    internal class MongoDBStorageServiceProvider : IStorageServiceProvider
    {
        #region Fields

        private static readonly MongoDatabaseSettings s_mongoSettings;

        private readonly ReaderWriterLockSlim _storageLocker;

        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDB;
        private readonly MongoDBConfiguration _mongoConfig;

        private readonly ImmutableDictionary<Type, string> _collectionNames;
        private ImmutableDictionary<Type, IStorageService> _storageServices;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="MongoDBStorageServiceProvider"/>
        /// </summary>
        static MongoDBStorageServiceProvider()
        {
            s_mongoSettings = new MongoDatabaseSettings()
            {
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard,
            };
        }

        /// <summary>
        /// Initialized a new instance of the class <see cref="MongoDBStorageServiceProvider"/>
        /// </summary>
        public MongoDBStorageServiceProvider(IOptions<MongoDBConfiguration> configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            this._mongoConfig = configuration.Value;

            this._storageLocker = new ReaderWriterLockSlim();
            this._storageServices = ImmutableDictionary<Type, IStorageService>.Empty;

#if DEBUG
            var types = this._mongoConfig.CollectionNames.SelectMany(c => c.Value).Select(k => new { Type = Type.GetType(k), Key = k }).ToArray();

            if (!types.All(t => t.Type != null))
            {
                var invalidTypes = types.Where(t => t.Type == null)
                                        .Select(t => t.Key)
                                        .ToArray();
                throw new InvalidOperationException("Type unknown : " + string.Join(", ", invalidTypes));
            }
#endif

            this._collectionNames = this._mongoConfig.CollectionNames.SelectMany(c => c.Value)
                                                                     .ToImmutableDictionary(k => Type.GetType(k),
                                                                                            v => this._mongoConfig.CollectionNames.First(t => t.Value.Contains(v)).Key);

            this._mongoClient = new MongoClient($"mongodb://{this._mongoConfig.Host}:{this._mongoConfig.Port}/{this._mongoConfig.DataBase}");

            this._mongoDB = this._mongoClient.GetDatabase(this._mongoConfig.DataBase, s_mongoSettings);

            System.Collections.Generic.IEnumerable<string> missingCollection = this._collectionNames.Select(c => c.Value)
                                                    .Distinct()
                                                    .Except(this._mongoDB.ListCollectionNames().ToList());

            foreach (string missing in missingCollection)
                this._mongoDB.CreateCollection(missing);
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// Gets the specific storage for the specific <typeparamref name="TEntity"/>
        /// </summary>
        public IStorageService<TEntity> GetStorageService<TEntity>()
            where TEntity : IEntity
        {
            Type key = typeof(TEntity);
            using (this._storageLocker.LockRead())
            {
                if (this._storageServices.TryGetValue(key, out IStorageService storageService))
                    return (IStorageService<TEntity>)storageService;
            }

            using (this._storageLocker.LockWrite())
            {
                if (this._storageServices.TryGetValue(key, out IStorageService storageService))
                    return (IStorageService<TEntity>)storageService;

                string collectionName = key.Name;

                if (this._collectionNames.TryGetValue(key, out string configKey))
                    collectionName = configKey;

                // Ensure the mongo drive can map the current entity equired
                //DomainMongoMapper.Map<TEntity>();

                var newStorageService = new MongoDBStorageService<TEntity>(this._mongoDB.GetCollection<TEntity>(collectionName));
                this._storageServices = this._storageServices.Add(key, newStorageService);

                return newStorageService;
            }
        }

        #endregion
    }
}
