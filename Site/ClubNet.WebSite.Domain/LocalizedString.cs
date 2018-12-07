using System.Collections.Generic;

namespace ClubNet.WebSite.Domain
{
    /// <summary>
    /// Define a localized string
    /// </summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{System.String, System.String}" />
    public sealed class LocalizedString : Dictionary<string, string>
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedString"/> class.
        /// </summary>
        public LocalizedString(string key, string value)
        {
            Add(key, value);
        }

        #endregion
    }
}
