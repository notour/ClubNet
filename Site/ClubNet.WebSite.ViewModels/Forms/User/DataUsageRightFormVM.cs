using System.ComponentModel.DataAnnotations;
using ClubNet.WebSite.Common.Contracts;

namespace ClubNet.WebSite.ViewModels.Forms.User
{
    /// <summary>
    /// Define the form information giving the usage right on the member informations
    /// </summary>
    public sealed class DataUsageRightFormVM : BaseFormVM
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Initiazalize a new instance of the class <see cref="DataUsageRightFormVM"/>
        /// </summary>
        public DataUsageRightFormVM(IRequestService requestService)
            : base(requestService)
        {
            ImageUsageInternal = true;
            NameUsageInternal = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating if the member image could be use in internal way.
        /// </summary>
        public bool ImageUsageInternal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the member image could be use in public way.
        /// </summary>
        public bool ImageUsagePublic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the member name (firstname et lastname) could be use in public communication.
        /// </summary>
        public bool NameUsagePublic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the member name (firstname et lastname) could be use in internal communication.
        /// </summary>
        public bool NameUsageInternal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the member have correclty read on confirm understand the impact of his choice
        /// </summary>
        [Required]
        public bool HaveBeenReaded { get; set; }

        #endregion
    }
}
