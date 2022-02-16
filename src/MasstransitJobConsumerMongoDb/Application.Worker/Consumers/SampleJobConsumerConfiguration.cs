using System;
using Application.Worker.Message;
using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using MassTransit.JobService;

namespace Application.Worker.Consumers
{
    public class SampleJobConsumerConfiguration : ConsumerDefinition<SampleJobConsumer>
    {
        public SampleJobConsumerConfiguration()
        {
            EndpointName = "queue-sample";
        }

        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<SampleJobConsumer> consumerConfigurator)
        {
            consumerConfigurator.Options<JobOptions<ISampleMessage>>(options =>
                options.SetRetry(r => r.Interval(3, TimeSpan.FromSeconds(30))).SetJobTimeout(TimeSpan.FromMinutes(10)).SetConcurrentJobLimit(10));
        }
    }
}