using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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
