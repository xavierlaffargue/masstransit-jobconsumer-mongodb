using Application.Worker.Consumers;

namespace Application.Worker.Extensions
{
    using Application.Worker.Options;
    using MassTransit;
    using MassTransit.JobService.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public static class ServicesExtensions
    {
        public static void AddLogger(this IServiceCollection services)
        {
            var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            services.AddLogging(logging => logging.AddSerilog(logger, true));
        }

        public static void AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBus = configuration.GetSection(AzureServiceBusSection.Name).Get<AzureServiceBusSection>();

            services.AddMassTransit(x =>
            {
                x.AddServiceBusMessageScheduler();

                x.AddConsumer<SampleJobConsumer, SampleJobConsumerConfiguration>();

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(serviceBus.ConnectionString);
                    cfg.UseServiceBusMessageScheduler();

                    var options = new ServiceInstanceOptions()
                        .EnableJobServiceEndpoints();

                    cfg.ServiceInstance(options, instance =>
                    {
                        instance.ConfigureJobService();
                        instance.ConfigureEndpoints(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}