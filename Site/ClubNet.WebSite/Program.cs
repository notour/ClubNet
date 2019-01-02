namespace ClubNet.WebSite
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Program entry point
    /// </summary>
    public class Program
    {
        #region Ctor

        /// <summary>
        /// Initialized the new instance of the class <see cref="Program"/>
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create the specific builder
        /// </summary>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .ConfigureLogging((hostingContext, logging) =>
                          {
                              logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                              logging.AddConsole();
                              logging.AddDebug();
                          })
                          .UseStartup<Startup>();
        }

        #endregion

    }
}
