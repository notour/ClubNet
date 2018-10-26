using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ClubNet.WebSite.Common.Errors
{
    /// <summary>
    /// Define all the error codes that occured in the internal site managements
    /// </summary>
    public enum InternalErrorCodes
    {
        None = 0,
        Conflict = 409,
        InternalError = 500
    }

    /// <summary>
    /// Provide tools to transform <see cref="InternalErrorCodes"/>
    /// </summary>
    public static class InternalErrorCodesExtension
    {
        /// <summary>
        /// Get the string of the error code
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetCodeString(this InternalErrorCodes internalErrorCode)
        {
            return ((int)internalErrorCode).ToString();
        }
    }
}
