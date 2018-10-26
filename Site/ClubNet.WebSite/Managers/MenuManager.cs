﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using ClubNet.WebSite.Interface.Domain;

namespace ClubNet.WebSite.Managers
{
    /// <summary>
    /// Managed the menu display & configuration in function of the user informations
    /// </summary>
    public sealed class MenuManager
    {
        #region Fields

        private readonly ImmutableDictionary<string, MenuVM> s_menuCache;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MenuManager"/>
        /// </summary>
        public MenuManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

    }
}