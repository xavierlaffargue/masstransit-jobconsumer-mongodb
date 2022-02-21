using Application.Worker.Consumers;
using MassTransit.JobService.Components.StateMachines;
using Orchestrator.JobConsumer.Options;

namespace Application.Worker.Extensions
{
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
            var mongodb = configuration.GetSection(MongoDbSection.Name).Get<MongoDbSection>();
            var serviceBus = configuration.GetSection(AzureServiceBusSection.Name).Get<AzureServiceBusSection>();

            services.AddMassTransit(x =>
            {
                x.AddServiceBusMessageScheduler();

                x.AddSagaRepository<JobSaga>()
                    .MongoDbRepository(r =>
                    {
                        r.Connection = mongodb.ConnectionString;
                        r.DatabaseName = mongodb.Database;
                    });

                x.AddSagaRepository<JobTypeSaga>()
                    .MongoDbRepository(r =>
                    {
                        r.Connection = mongodb.ConnectionString;
                        r.DatabaseName = mongodb.Database;
                    });

                x.AddSagaRepository<JobAttemptSaga>()
                    .MongoDbRepository(r =>
                    {
                        r.Connection = mongodb.ConnectionString;
                        r.DatabaseName = mongodb.Database;
                    });

                x.AddConsumer<SampleJobConsumer, SampleJobConsumerConfiguration>();

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(serviceBus.ConnectionString);
                    cfg.UseServiceBusMessageScheduler();

                    var options = new ServiceInstanceOptions()
                        .EnableJobServiceEndpoints();

                    cfg.ServiceInstance(options, instance =>
                    {
                        instance.ConfigureJobServiceEndpoints(js =>
                        {
                            js.FinalizeCompleted = serviceBus.FinalizeCompleted;
                            js.SagaPartitionCount = serviceBus.SagaPartitionCount;
                            js.ConfigureSagaRepositories(context);
                        });
                        instance.ConfigureEndpoints(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}