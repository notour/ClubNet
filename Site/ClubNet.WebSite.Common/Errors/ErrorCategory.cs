using System;
using System.Collections.Generic;
using System.Text;

namespace ClubNet.WebSite.Common.Errors
{
    /// <summary>
    /// Define all the category where an error could occured
    /// </summary>
    public enum ErrorCategory
    {
        None,
        User,
        Login,
        Register,
        Global,
    }
}
