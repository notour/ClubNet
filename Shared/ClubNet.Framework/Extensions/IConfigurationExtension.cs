using System;
using System.Linq;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Extend the <see cref="IConfiguration"/> service
    /// </summary>
    public static class IConfigurationExtension
    {
        #region Methods

        /// <summary>
        /// Load and instanciate the configuration <typeparamref name="TConfig"/>
        /// </summary>
        public static TConfig GetConfig<TConfig>(this IConfiguration config, string sectionKey)
        {
            var section = config.GetSection(sectionKey + ":*");

            throw new NotImplementedException();

            return default(TConfig);
        }

        #endregion
    }
}
