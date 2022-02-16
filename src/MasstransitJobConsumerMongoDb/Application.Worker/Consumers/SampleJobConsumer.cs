using System.Threading.Tasks;
using Application.Worker.Message;
using MassTransit.JobService;
using Microsoft.Extensions.Logging;

namespace Application.Worker.Consumers
{
    public class SampleJobConsumer : IJobConsumer<ISampleMessage>
    {
        private readonly ILogger<SampleJobConsumer> _logger;

        public SampleJobConsumer(ILogger<SampleJobConsumer> logger)
        {
            this._logger = logger;
        }

        public async Task Run(JobContext<ISampleMessage> context)
        {
            _logger.LogInformation($"Received SecureMedia job id : {context.JobId}");

            _logger.LogInformation($"Job {context.JobId} completed");
        }
    }
}