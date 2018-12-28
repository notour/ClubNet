namespace ClubNet.WebSite.NusinessLayer.Extensions
{
    using ClubNet.WebSite.BusinessLayer.Contracts;
    using ClubNet.WebSite.BusinessLayer.Services;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Extend the <see cref="IHttpContextAccessor"/> to managed request services
    /// </summary>
    public static class IHttpContextAccessorExtension
    {
        #region Methods

        /// <summary>
        /// Extract or create the current request service
        /// </summary>
        public static IRequestService CurrentRequestService(this IHttpContextAccessor httpContextAccessor)
        {
            IRequestService reqService = null;
            if (!httpContextAccessor.HttpContext.Items.TryGetValue(nameof(IRequestService), out var service))
            {
                var ctx = httpContextAccessor.HttpContext;
                lock (ctx)
                {
                    if (!ctx.Items.TryGetValue(nameof(IRequestService), out service))
                    {
                        reqService = new RequestServiceImpl(ctx);
                        ctx.Items.Add(nameof(IRequestService), reqService);
                    }
                }
            }
            else
            {
                reqService = (IRequestService)service;
            }

            return reqService;
        }

        #endregion
    }
}
