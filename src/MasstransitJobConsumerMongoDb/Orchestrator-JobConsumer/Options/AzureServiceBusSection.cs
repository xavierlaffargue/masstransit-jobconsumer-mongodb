namespace Orchestrator.JobConsumer.Options
{
    public class AzureServiceBusSection
    {
        public const string Name = "AzureServiceBus";

        public string ConnectionString { get; set; }

        public bool FinalizeCompleted { get; set; }

        public int SagaPartitionCount { get; set; }
    }
}