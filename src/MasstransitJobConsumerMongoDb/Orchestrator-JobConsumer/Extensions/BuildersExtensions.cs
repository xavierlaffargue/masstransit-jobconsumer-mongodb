using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Orchestrator.JobConsumer.Extensions
{
    public static class BuildersExtensions
    {
        public static void ConfigureHost(this IConfigurationBuilder builder)
        {

        }

        public static void ConfigureApp(this HostBuilderContext context, IConfigurationBuilder configuration)
        {

        }

        public static void ConfigureServices(this HostBuilderContext builder, IServiceCollection services)
        {
            services.AddLogger();
            services.AddMassTransit(builder.Configuration);
        }
    }
}
