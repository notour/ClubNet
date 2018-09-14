using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ClubNet.WebSite.DataLayer.Configurations
{
    /// <summary>
    /// Section in charge of handling configuration related to a specific type to store in mongo
    /// </summary>
    public sealed class MongoDBConfiguration
    {
        #region Fields

        public const string ConfigurationSectionKey = "MongoDB";
        
        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MongoDBConfiguration"/>
        /// </summary>
        public MongoDBConfiguration(string host, string dataBase, IDictionary<string, IEnumerable<Type>> collectionNames, int port = 21017)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(Host));

            if (string.IsNullOrEmpty(dataBase))
                throw new ArgumentNullException(nameof(DataBase));

            if (port < 0)
                throw new ArgumentException($"Port invalid : ${port}");

            Host = host;
            DataBase = dataBase;
            Port = port;
            CollectionNames = collectionNames.ToImmutableDictionary();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the association between the entity type and the collection
        /// </summary>
        public IDictionary<string, IEnumerable<Type>> CollectionNames { get; }

        /// <summary>
        /// Gets the database name
        /// </summary>
        public string DataBase { get; }

        /// <summary>
        /// Gets the database port
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Gets the database host
        /// </summary>
        public string Host { get; }

        #endregion
    }
}
