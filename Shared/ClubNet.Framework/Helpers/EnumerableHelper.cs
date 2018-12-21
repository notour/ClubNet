using System;
using System.Collections.Generic;
using System.Text;

namespace ClubNet.Framework.Helpers
{
    public static class EnumerableHelper<TEntity>
    {
        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="EnumerableHelper{TEntity}/>
        /// </summary>
        static EnumerableHelper()
        {
            Empty = new TEntity[0];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the the empty collection
        /// </summary>
        public static IEnumerable<TEntity> Empty { get; }

        #endregion
    }
}
