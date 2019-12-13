namespace Aranasoft.Cobweb.Azure.Configuration {
    public interface IAzureConfiguration {
        string ServiceBusConnectionString { get; }
        string StorageConnectionString { get; }
        string QueueName { get; }
    }
}
