namespace ClubNet.WebSite.ViewModels.Forms.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ClubNet.Shared.Api.Dto;
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Domain.User;

    /// <summary>
    /// New subscription form
    /// </summary>
    public sealed class NewSubscriptionFormVM : BaseFormVM, INewSubscriptionBaseDto
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="NewSubscriptionFormVM"/>
        /// </summary>
        /// <param name="requestService"></param>
        public NewSubscriptionFormVM(IRequestService requestService)
            : base(requestService)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the member id target by this current subscription
        /// </summary>
        public Guid? MemberId { get; private set; }

        /// <summary>
        /// Gets the current season associate to the current subscription
        /// </summary>
        public Guid SeasonId { get; private set; }

        /// <summary>
        /// Gets the generated member club id
        /// </summary>
        public string MemberNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && BirthDate != null)
                    return $"{BirthDate.Value.Year}{BirthDate.Value.Month}{BirthDate.Value.Day}{FirstName.ToUpperInvariant()[0]}{LastName.ToUpperInvariant()[0]}";
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the member sexe
        /// </summary>
        [EnumDataType(typeof(SexeEnum))]
        public SexeEnum Sexe { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true)]
        [DataType(DataType.Text)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the member birth place
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string BirthPlace { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mail address
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the address street
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the address complement information
        /// </summary>
        [DataType(DataType.Text)]
        public string StreetComplement { get; set; }

        /// <summary>
        /// Gets or sets the current postal code
        /// </summary>
        [Required]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the member city 
        /// </summary>
        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the role collections
        /// </summary>
        [Required]
        public IEnumerable<string> Roles { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Setup the current view model
        /// </summary>
        public void SetupForm(Guid? memberId, Guid seasonId)
        {
            this.MemberId = memberId;
            this.SeasonId = seasonId;
        }

        #endregion
    }
}
