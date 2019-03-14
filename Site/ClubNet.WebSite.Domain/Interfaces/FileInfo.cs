namespace ClubNet.WebSite.Domain.Interfaces
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using ClubNet.WebSite.Domain.Security;

    /// <summary>
    /// Define the basic informations for a specific file access
    /// </summary>
    [DataContract]
    public abstract class FileInfo : Entity<FileInfoType>
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="FileInfo"/>
        /// </summary>
        protected FileInfo(FileInfoType fileInfoType)
            : base(fileInfoType)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the file original path
        /// </summary>
        [DataMember(IsRequired = true)]
        public string OriginalPath { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new file info
        /// </summary>
        protected void Create(string originalPath, SecurityCriteria securityCriteria, bool isDraft)
        {
            base.Create(securityCriteria, isDraft);
            OriginalPath = originalPath;
        }

        /// <summary>
        /// Update the current file info
        /// </summary>
        protected void Update(string originalPath, SecurityCriteria securityCriteria, bool isDraft)
        {
            base.Update(securityCriteria, isDraft);
            OriginalPath = originalPath;
        }

        #endregion
    }
}
