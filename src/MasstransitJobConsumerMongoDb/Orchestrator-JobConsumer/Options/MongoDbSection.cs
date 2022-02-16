namespace Orchestrator.JobConsumer.Options
{
    public class MongoDbSection
    {
        public const string Name = "MongoDb";

        public string ConnectionString { get; set; }

        public string Database { get; set; }
    }
}
