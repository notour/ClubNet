using System;
using System.Collections.Generic;
using System.Text;

namespace ClubNet.WebSite.BusinessLayer.Contracts
{
    public interface IThemeBL
    {
        /// <summary>
        /// Gets the current theme name
        /// </summary>
        string CurrentThemeName { get; }
    }
}
