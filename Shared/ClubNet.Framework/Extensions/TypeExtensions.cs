namespace System
{
    /// <summary>
    /// Extend the type class
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Define if the current type is scalar
        /// </summary>
        public static bool IsScalar(this Type type)
        {
            if (type == typeof(string) ||
                type == typeof(bool) ||
                type == typeof(bool?) ||
                type == typeof(int) ||
                type == typeof(Int16) ||
                type == typeof(Int32) ||
                type == typeof(Int64) ||
                type == typeof(decimal) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(Guid) ||
                type == typeof(int?) ||
                type == typeof(Int16?) ||
                type == typeof(Int32?) ||
                type == typeof(Int64?) ||
                type == typeof(decimal?) ||
                type == typeof(float?) ||
                type == typeof(double?) ||
                type == typeof(Guid?))
                return true;
            return false;
        }
    }
}
