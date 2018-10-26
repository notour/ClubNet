using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClubNet.WebSite.Common.Contracts;
using ClubNet.WebSite.Common.Errors;
using ClubNet.WebSite.DataLayer;
using ClubNet.WebSite.Domain.Logs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace ClubNet.WebSite.Services.Impl
{
    /// <summary>
    /// Define a service that managed all the error throught the application
    /// </summary>
    class ErrorServiceImpl : IErrorService
    {
        #region Fields

        private readonly IStorageService<ErrorLog> _storageService;
        private readonly IResourceService _resourceService;
        private readonly string _url;
        private string _userId;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ErrorServiceImpl" />
        /// </summary>
        public ErrorServiceImpl(IResourceService resourceService, IHttpContextAccessor contextAccessor, IStorageServiceProvider storageService)
        {
            var ctx = contextAccessor.HttpContext;
            _url = ctx.Request.Path.ToUriComponent();

            Task.Run(async () => await ctx.AuthenticateAsync()).ContinueWith(a =>
            {
                if (a.Result != null && a.Result.Succeeded && a.Result.Principal != null && a.Result.Principal.Identity != null && a.Result.Principal.Identity.IsAuthenticated)
                    _userId = a.Result.Principal.Identity.Name;
            });

            _resourceService = resourceService;
            _storageService = storageService.GetStorageService<ErrorLog>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provide the error description
        /// </summary>
        public string GetErrorDescription(ErrorCategory errorCategory, InternalErrorCodes errorCode, string contextKey, params string[] context)
        {
            var errorStr = _resourceService.GetString(errorCategory, errorCode + "_" + contextKey);

            if (context != null && context.Any())
                errorStr = string.Format(errorStr, context);

            return errorStr;
        }

        /// <summary>
        /// Provide an error using the id in it
        /// </summary>
        public string GetErrorDescription(ErrorCategory errorCategory, InternalErrorCodes errorCode, Guid errorLoggedId)
        {
            var errorStr = _resourceService.GetString(ErrorCategory.Logged, string.Empty);
            errorStr = string.Format(errorStr, errorCategory, errorCode, errorLoggedId);

            return errorStr;
        }

        /// <summary>
        /// Log the specific error and associate an id to it
        /// </summary>
        public Guid LogError(ErrorCategory errorCategory, InternalErrorCodes errorCode, string contextKey, params string[] context)
        {
            var errorLog = new ErrorLog()
            {
                Id = Guid.NewGuid(),
                Category = Common.Enums.LogCategory.Error,
                ContextKey = contextKey,
                ErrorCode = errorCode,
                ErrorCategory = errorCategory,
                RequestUrl = _url,
                UserId = _userId,
                WhenUtc = DateTime.UtcNow,
                DisplayMessage = GetErrorDescription(errorCategory, errorCode, contextKey, context),
                Contexts = context,
            };
            var stackTrace = CompressedStack.Capture();
            errorLog.WhereUtc = stackTrace.ToString();

            Task.Run(() => _storageService.CreateAsync(errorLog, null, CancellationToken.None));

            return errorLog.Id;
        }

        /// <summary>
        /// Log the specific error and associate an id to it
        /// </summary>
        public Guid LogError(ErrorCategory errorCategory, InternalErrorCodes errorCode, string contextKey, Exception ex, params string[] context)
        {
            var exceptions = new List<string>();
            while (ex != null)
            {
                exceptions.Add(ex.Message);
                ex = ex.InnerException;
            }

            return LogError(errorCategory, errorCode, contextKey, context.Concat(new[] { string.Join(" <-- ", exceptions) }).ToArray());
        }

        #endregion
    }
}
