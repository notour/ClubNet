namespace ClubNet.Shared.Api.Dto
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a dto object to store all the field when creating a new account
    /// </summary>
    [DataContract]
    public class RegisterDto : IRegisterModel
    {
        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user confirmation password
        /// </summary>
        [DataMember]
        public string ConfirmationPassword { get; set; }
    }
}
