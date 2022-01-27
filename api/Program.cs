using System;
using System.IO;
using System.Net;
using System.Reflection;
using Figgle;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace api
{
    class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Info(
                    "Starting service .... .... ...\n" + FiggleFonts.Standard.Render("Odata   Experiment"));
                var webHost = CreateWebHostBuilder(args).Build();
                webHost.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Application start-up failed with exception: {ex.Message}");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        options.Listen(IPAddress.Loopback, 5000);
                    }
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();

                    var config = builder.Build();
                    builder.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);
                    LogManager.GetCurrentClassLogger().Info("Running locally using user secrets");
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .CaptureStartupErrors(true)
                .UseNLog();
    }
}
