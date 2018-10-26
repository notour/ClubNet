using Newtonsoft.Json;

namespace System.Diagnostics
{
    /// <summary>
    /// Extend all the object to add diagnostics helpers
    /// </summary>
    public static class ObjectExtensions
    {
        #region Methods

        /// <summary>
        /// Transform in json the current object
        /// </summary>
        public static string ToDiagnosticJson(this object obj)
        {
            if (obj == null)
                return string.Empty;
            return JsonConvert.SerializeObject(obj);
        }

        #endregion
    }
}
