using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ClubNet.WebSite.Domain.User;

namespace ClubNet.WebSite.DataLayer
{
    /// <summary>
    /// Define a storage service associate to the type <typeparamref name="TEntity"/>
    /// </summary>
    public interface IStorageService
    {
    }

    /// <summary>
    /// Define a storage service associate to the type <typeparamref name="TEntity"/>
    /// </summary>
    public interface IStorageService<TEntity> : IStorageService
    {
        #region Methods

        /// <summary>
        /// Find the first result that match the filter
        /// </summary>
        Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

        /// <summary>
        /// Find the first result that match the filter
        /// </summary>
        Task<TProjection> FindFirstAsync<TProjection>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

        /// <summary>
        /// Create a new item
        /// </summary>
        Task<TEntity> CreateAsync(TEntity user, CancellationToken cancellationToken);

        /// <summary>
        /// Create a new item it an existing one exist with the unicity selector then return it except if the parameter thownOnConflict is set to true
        /// </summary>
        Task<TEntity> CreateAsync(TEntity user, Expression<Func<TEntity, bool>> unicitySelector, CancellationToken cancellationToken, bool thownOnConflict = false);

        /// <summary>
        /// Save an existing item
        /// </summary>
        Task<TEntity> Save(TEntity user, CancellationToken cancellationToken);

        /// <summary>
        /// Find all the occurence that math the expression
        /// </summary>
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

        /// <summary>
        /// Find all the occurence that math the expression
        /// </summary>
        Task<IEnumerable<TProjection>> FindAllAsync<TProjection>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

        #endregion
    }
}
