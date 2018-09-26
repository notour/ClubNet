using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ClubNet.Framework.Attributes;

namespace ClubNet.WebSite.DataLayer.Configurations
{
    /// <summary>
    /// Section in charge of handling configuration related to a specific type to store in mongo
    /// </summary>
    [ConfigurationData(ConfigurationSectionKey)]
    public sealed class MongoDBConfiguration
    {
        #region Fields

        public const string ConfigurationSectionKey = "MongoDB";

        #endregion

        #region Ctor

        ///// <summary>
        ///// Initialize a new instance of the class <see cref="MongoDBConfiguration"/>
        ///// </summary>
        public MongoDBConfiguration()
        {
            Port = 27017;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the association between the entity type and the collection
        /// </summary>
        public IDictionary<string, IEnumerable<string>> CollectionNames { get; set; }

        /// <summary>
        /// Gets the database name
        /// </summary>
        public string DataBase { get; set; }

        /// <summary>
        /// Gets the database port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets the database host
        /// </summary>
        public string Host { get; set; }

        #endregion
    }
}
