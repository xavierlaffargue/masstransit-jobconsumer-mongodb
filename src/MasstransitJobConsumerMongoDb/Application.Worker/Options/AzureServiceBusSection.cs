namespace Application.Worker.Options
{
    public class AzureServiceBusSection
    {
        public const string Name = "AzureServiceBus";

        public string ConnectionString { get; set; }
    }
}