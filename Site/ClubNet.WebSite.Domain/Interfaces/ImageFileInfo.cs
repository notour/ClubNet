namespace ClubNet.WebSite.Domain.Interfaces
{
    using System.Collections.Generic;

    using ClubNet.WebSite.Common.Enums;
    using ClubNet.WebSite.Domain.Security;

    /// <summary>
    /// Define a specific file type image
    /// </summary>
    public sealed class ImageFileInfo : FileInfo
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ImageFileInfo"/>
        /// </summary>
        public ImageFileInfo()
            : base(FileInfoType.Image)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specific platform path of the picture
        /// </summary>
        public IReadOnlyDictionary<TargetPlateform, string> PlatformedPath { get; private set; }

        /// <summary>
        /// Gets the image ratio H/W
        /// </summary>
        public float Ratio { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new image
        /// </summary>
        public static ImageFileInfo Create(string originalPath, IReadOnlyDictionary<TargetPlateform, string> platformedPath, float ratio, SecurityCriteria securityCriteria)
        {
            var inst = new ImageFileInfo();
            inst.Create(originalPath, securityCriteria);
            inst.SetData(platformedPath, ratio);
            return inst;
        }

        /// <summary>
        /// Update the current image info
        /// </summary>
        public void Update(string originalPath, IReadOnlyDictionary<TargetPlateform, string> platformedPath, float ratio, SecurityCriteria securityCriteria)
        {
            base.Update(originalPath, securityCriteria);
            SetData(platformedPath, ratio);
        }

        /// <summary>
        /// Setup the current image data
        /// </summary>
        private void SetData(IReadOnlyDictionary<TargetPlateform, string> platformedPath, float ratio)
        {
            this.PlatformedPath = platformedPath;
            this.Ratio = ratio;
        }

        #endregion
    }
}
