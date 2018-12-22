using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using ClubNet.WebSite.Domain.Attributes;
using MongoDB.Bson.Serialization;

namespace ClubNet.WebSite.DataLayer.Tools
{
    /// <summary>
    /// Mapper in charge to create a mapping MongoDB from the domaine entities
    /// </summary>
    public static class DomainMongoMapper
    {
        #region Fields

        private const string DefaultIdProperty = "Id";

        #endregion

        #region Methods

        /// <summary>
        /// Map the current entity type
        /// </summary>
        /// <remarks>
        ///     It managed the multiple mapping
        /// </remarks>
        public static void Map<TEntity>()
        {
            var entityType = typeof(TEntity);
            var map = GenerateEntityMap(entityType);

            if (entityType.IsAbstract)
            {
                foreach (var attr in entityType.GetCustomAttributes<KnownTypeAttribute>())
                    GenerateEntityMap(attr.Type);
            }
        }

        /// <summary>
        /// Generate an unfreeze map
        /// </summary>
        private static BsonClassMap GenerateEntityMap(Type entityType)
        {
            if (BsonClassMap.IsClassMapRegistered(entityType))
                return BsonClassMap.GetRegisteredClassMaps().First(r => r.ClassType == entityType);

            if (entityType.IsValueType || entityType == typeof(string) || entityType == typeof(Type) || entityType == typeof(Guid) || entityType.IsEnum || entityType == typeof(object) ||
                (entityType.IsGenericType && entityType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                return null;

            BsonClassMap baseClassMap = null;

            if (entityType.BaseType != null)
                baseClassMap = GenerateEntityMap(entityType.BaseType);

            var map = new BsonClassMap(entityType, baseClassMap);
            map.SetIgnoreExtraElements(true);

            if (entityType.IsAbstract)
            {
                foreach (var attr in entityType.GetCustomAttributes<KnownTypeAttribute>())
                    map.AddKnownType(attr.Type);
            }

            var mapProperties = (from prop in entityType.GetProperties()
                                 where prop.DeclaringType == entityType
                                 let propDataMember = prop.GetCustomAttribute<DataMemberAttribute>()
                                 let propId = prop.GetCustomAttribute<IdAttribute>()
                                 let ignoreAttr = prop.GetCustomAttribute<IgnoreDataMemberAttribute>()
                                 select new { prop, propDataMember, propId, ignoreAttr }).ToArray();

            foreach (var mapProperty in mapProperties)
            {
                var propType = mapProperty.prop.PropertyType;
                if (mapProperty.prop.DeclaringType != entityType)
                    continue;

                if (mapProperty.propId != null || mapProperty.prop.Name == DefaultIdProperty)
                {
                    map.MapIdProperty(mapProperty.prop.Name)
                       .SetIsRequired(true);
                }
                else if (mapProperty.ignoreAttr == null)
                {
                    if (typeof(IEnumerable).IsAssignableFrom(propType))
                    {
                        var collectionType = (from i in propType.GetInterfaces().Append(propType)
                                              where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                              select i)
                                              .FirstOrDefault()
                                              ?.GetGenericArguments().FirstOrDefault();

                        if (collectionType != null && propType != collectionType)
                        {
                            GenerateEntityMap(collectionType);
                        }
                    }

                    var prop = map.MapProperty(mapProperty.prop.Name);

                    if (mapProperty.propDataMember != null)
                    {
                        prop.SetIsRequired(mapProperty.propDataMember.IsRequired)
                            .SetIgnoreIfDefault(!mapProperty.propDataMember.EmitDefaultValue);
                    }
                }
            }

            map = map.Freeze();
            if (!BsonClassMap.IsClassMapRegistered(entityType))
                BsonClassMap.RegisterClassMap(map);

            return map;
        }

        #endregion
    }
}
