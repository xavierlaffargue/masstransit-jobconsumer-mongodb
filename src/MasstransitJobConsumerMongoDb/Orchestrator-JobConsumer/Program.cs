using Microsoft.Extensions.Hosting;
using Orchestrator.JobConsumer.Extensions;

namespace Orchestrator.JobConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(BuildersExtensions.ConfigureHost)
                .ConfigureAppConfiguration(BuildersExtensions.ConfigureApp)
                .ConfigureServices(BuildersExtensions.ConfigureServices);
        }
    }
}
