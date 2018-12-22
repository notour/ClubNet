namespace ClubNet.WebSite.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedString"/> class.
        /// </summary>
        public LocalizedString()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current value in function of the current language configurated
        /// </summary>
        public string GetLocalizedValue(string lang)
        {
            if (TryGetValue(lang.ToUpperInvariant(), out var value))
                return value;

            if (TryGetValue(CultureInfo.InvariantCulture.TwoLetterISOLanguageName.ToUpperInvariant(), out var defaultValue))
                return defaultValue;

            return string.Empty;
        }

        #endregion
    }
}
