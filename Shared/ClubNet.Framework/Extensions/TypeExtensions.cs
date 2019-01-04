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
                type == typeof(short) ||
                type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(decimal) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(Guid) ||
                type == typeof(int?) ||
                type == typeof(short?) ||
                type == typeof(long?) ||
                type == typeof(decimal?) ||
                type == typeof(float?) ||
                type == typeof(double?) ||
                type == typeof(Guid?))
                return true;
            return false;
        }
    }
}
