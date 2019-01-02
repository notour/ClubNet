namespace System
{
    /// <summary>
    /// Extend the string class
    /// </summary>
    public static class StringExtensions
    {
        #region Methods

        /// <summary>
        /// Format the string with arguments
        /// </summary>
        public static string WithArguments(this string str, params object[] arguments)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return string.Format(str, arguments);
        }

        #endregion
    }
}
