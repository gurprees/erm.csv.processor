using erm.CsvProcessor;
using erm.CsvProcessor.interfaces;
using erm.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;

namespace CsvProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Debug("DataProcessor start");

            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                var servicesProvider = BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    var startUp = servicesProvider.GetRequiredService<IStartUp>();
                    startUp.Start();

                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
                
               .AddLogging(loggingBuilder =>
               {
                   loggingBuilder.ClearProviders();
                   loggingBuilder.AddNLog(config);
               })
               .AddOptions()
               .Configure<AppSettings>(config)
               .AddTransient<IStartUp, StartUp>()
               .AddTransient<IProcessor, Processor>()
               .AddTransient<ICsvParser, CsvParser>()
               .AddTransient<IMedianProcessor, MedianProcessor>()
               .BuildServiceProvider();
        }
    }
}
