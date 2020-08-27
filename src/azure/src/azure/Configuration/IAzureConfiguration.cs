namespace Aranasoft.Cobweb.Azure.Configuration {
    public interface IAzureConfiguration {
        /// <summary>
        /// Service Bus Connection String
        /// </summary>
        string ServiceBusConnectionString { get; }
    }
}
