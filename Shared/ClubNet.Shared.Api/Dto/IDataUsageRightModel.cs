﻿namespace ClubNet.Shared.Api.Dto
{
    /// <summary>
    /// Define the form information giving the usage right on the member informations
    /// </summary>
    public interface IDataUsageRightModel
    {
        #region Properties


        /// <summary>
        /// Gets or sets a value indicating if the member image could be use in internal way.
        /// </summary>
        bool ImageUsageInternal { get; }

        /// <summary>
        /// Gets or sets a value indicating if the member image could be use in public way.
        /// </summary>
        bool ImageUsagePublic { get; }

        /// <summary>
        /// Gets or sets a value indicating if the member name (firstname et lastname) could be use in public communication.
        /// </summary>
        bool NameUsagePublic { get; }

        /// <summary>
        /// Gets or sets a value indicating if the member name (firstname et lastname) could be use in internal communication.
        /// </summary>
        bool NameUsageInternal { get; }

        /// <summary>
        /// Gets or sets a value indicating that the member have correclty read on confirm understand the impact of his choice
        /// </summary>
        bool HaveBeenReaded { get; }

        #endregion
    }
}
