using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.Configuration;
using Aranasoft.Cobweb.Azure.ServiceBus.Extensions;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace Aranasoft.Cobweb.Azure.ServiceBus {
    public abstract class Queue {
        private ServiceBusAdministrationClient _managementClient;
        private ServiceBusSender _queueClient;
        private ServiceBusProcessor _queueProcessor;
        private bool _queueValidated;

        protected string Name { get; }
        protected string ConnectionString { get; }

        protected Queue(string connectionString, string queueName) {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            Name = queueName ?? throw new ArgumentNullException(nameof(queueName));
        }

        protected Queue(IAzureConfiguration azureConfiguration, string queueName) {
            if (azureConfiguration == null) {
                throw new ArgumentNullException(nameof(azureConfiguration));
            }

            ConnectionString = azureConfiguration.ServiceBusConnectionString;
            Name = queueName ?? throw new ArgumentNullException(nameof(queueName));
        }

        protected ServiceBusAdministrationClient ManagementClient {
            get {
                _managementClient = _managementClient ??= new ServiceBusAdministrationClient(ConnectionString);
                return _managementClient;
            }
        }

        protected async Task<ServiceBusSender> GetQueueClientAsync() {
            await EnsureQueueAsync();
            _queueClient = _queueClient ??= new ServiceBusClient(ConnectionString).CreateSender(Name);
            return _queueClient;
        }

        protected async Task<ServiceBusProcessor> GetQueueProcessorAsync() {
            await EnsureQueueAsync();
            _queueProcessor = _queueProcessor ??= new ServiceBusClient(ConnectionString).CreateProcessor(Name);
            return _queueProcessor;
        }


        protected async Task EnsureQueueAsync() {
            if (!_queueValidated && !await ManagementClient.QueueExistsAsync(Name)) {
                await ManagementClient.CreateQueueAsync(Name);
                _queueValidated = true;
            }
        }

        /// <summary>
        /// Sends a message to Service Bus.
        /// </summary>
        public async Task SendMessageAsync(ServiceBusMessage message) {
            var queueClient = await GetQueueClientAsync();
            await queueClient.SendMessageAsync(message);
        }

        /// <summary>
        /// Sends a list of messages to Service Bus.
        /// </summary>
        public async Task SendMessagesAsync(IEnumerable<ServiceBusMessage> messages) {
            var queueClient = await GetQueueClientAsync();
            await queueClient.SendMessagesAsync(messages);
        }

        public async Task RegisterErrorHandlerAsync(Func<ProcessErrorEventArgs, Task> exceptionCallback) {
            var queueClient = await GetQueueProcessorAsync();
            queueClient.ProcessErrorAsync += exceptionCallback;
        }

        public async Task RegisterMessageHandlerAsync(Func<ProcessMessageEventArgs, Task> callback) {
            var queueClient = await GetQueueProcessorAsync();
            queueClient.ProcessMessageAsync += callback;
        }

        private void SetEnqueueTime(ServiceBusMessage message, int delaySeconds) {
            message.Delay(TimeSpan.FromSeconds(delaySeconds));
        }

        private void SetEnqueueTime(IEnumerable<ServiceBusMessage> messages, int delaySeconds) {
            foreach (var message in messages) {
                message.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }
    }
}
