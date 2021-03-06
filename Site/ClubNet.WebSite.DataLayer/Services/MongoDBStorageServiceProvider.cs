﻿using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using ClubNet.WebSite.DataLayer.Configurations;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ClubNet.WebSite.DataLayer.Services
{
    /// <summary>
    /// Storage provider that connect configuration and <see cref="MongoDBStorageService{TEntity}"/> specific 
    /// </summary>
    class MongoDBStorageServiceProvider : IStorageServiceProvider
    {
        #region Fields

        private const string DataStorageConnectionString = "DefaultConnection";
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
        public MongoDBStorageServiceProvider(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _mongoConfig = configuration.GetConfig<MongoDBConfiguration>(MongoDBConfiguration.ConfigurationSectionKey);
            _collectionNames = _mongoConfig.CollectionNames.SelectMany(c => c.Value)
                                                           .ToImmutableDictionary(k => k, v => _mongoConfig.CollectionNames.First(t => t.Value.Contains(v)).Key);

            _mongoClient = new MongoClient($"mongodb://${_mongoConfig.Host}:${_mongoConfig.Port}/${_mongoConfig.DataBase}");

            _mongoDB = _mongoClient.GetDatabase(_mongoConfig.DataBase, s_mongoSettings);
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// Gets the specific storage for the specific <typeparamref name="TEntity"/>
        /// </summary>
        public IStorageService<TEntity> GetStorageService<TEntity>()
        {
            var key = typeof(TEntity);
            using (_storageLocker.LockRead())
            {
                if (this._storageServices.TryGetValue(key, out var storageService))
                    return (IStorageService<TEntity>)storageService;
            }

            using (_storageLocker.LockWrite())
            {
                if (this._storageServices.TryGetValue(key, out var storageService))
                    return (IStorageService<TEntity>)storageService;

                string collectionName = key.Name;

                if (_collectionNames.TryGetValue(key, out var configKey))
                    collectionName = configKey;

                var newStorageService = new MongoDBStorageService<TEntity>(_mongoDB.GetCollection<TEntity>(collectionName));
                this._storageServices = this._storageServices.Add(key, newStorageService);

                return newStorageService;
            }
        }

        #endregion
    }
}
