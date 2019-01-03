namespace ClubNet.WebSite.Common.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Exception raised when you try to save a document changed between
    /// </summary>
    public sealed class ConcurrencySaveException : Exception
    {
    }
}
