namespace ClubNet.WebSite.ViewModels.User
{
    using System.Collections.Generic;

    using ClubNet.WebSite.Common.Contracts;

    /// <summary>
    /// View model to exposed user basic info
    /// </summary>
    public sealed class UserInfoViewModel : BaseVM
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="UserInfoViewModel"/>
        /// </summary>
        public UserInfoViewModel(IRequestService requestService) 
            : base(requestService)
        {
            ImagePath = "/content/missing_img.png";
            Name = "Lorem Ipsum";
            Roles = new string[]
            {
                "Admin",
                "Secretaire",
                "Contributeur",
                "Entraineur"
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user image path
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user roles
        /// </summary>
        public IEnumerable<string> Roles { get; set; }

        #endregion
    }
}
