namespace ClubNet.WebSite.Controllers
{
    using ClubNet.WebSite.Common.Contracts;
    using ClubNet.WebSite.Common.Enums;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using System;
    using System.IO;

    /// <summary>
    /// File controller user to get all the file on the web site
    /// </summary>
    [AllowAnonymous]
    public class FileController : Controller
    {
        #region Fields

        private readonly IFileService _fileService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="FileController"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        public FileController(IFileService fileService) 
        {
            _fileService = fileService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a resource image
        /// </summary>
        [HttpGet]
        public IActionResult Image(SiteResources siteResource)
        {
            var filePath = _fileService.GetHtmlPath(siteResource);
            return new PhysicalFileResult("~" + filePath, "image/" + Path.GetExtension(filePath));
        }

        #endregion
    }
}
