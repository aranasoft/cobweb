using System;
using System.Collections.Generic;
using System.Threading;
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
                _managementClient ??= new ServiceBusAdministrationClient(ConnectionString);
                return _managementClient;
            }
        }

        protected async Task<ServiceBusSender> GetQueueClientAsync(CancellationToken cancellationToken = default) {
            await EnsureQueueAsync(cancellationToken);
            return _queueClient ??= new ServiceBusClient(ConnectionString).CreateSender(Name);
        }

        protected async Task<ServiceBusProcessor> GetQueueProcessorAsync(CancellationToken cancellationToken = default) {
            await EnsureQueueAsync(cancellationToken);
            _queueProcessor ??= new ServiceBusClient(ConnectionString).CreateProcessor(Name);
            return _queueProcessor;
        }


        protected async Task EnsureQueueAsync(CancellationToken cancellationToken = default) {
            if (!_queueValidated && !await ManagementClient.QueueExistsAsync(Name, cancellationToken)) {
                await ManagementClient.CreateQueueAsync(Name, cancellationToken);
                _queueValidated = true;
            }
        }

        /// <summary>
        /// Sends a message to Service Bus.
        /// </summary>
        public async Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken = default) {
            var queueClient = await GetQueueClientAsync(cancellationToken);
            await queueClient.SendMessageAsync(message, cancellationToken);
        }

        /// <summary>
        /// Sends a list of messages to Service Bus.
        /// </summary>
        public async Task SendMessagesAsync(IEnumerable<ServiceBusMessage> messages, CancellationToken cancellationToken = default) {
            var queueClient = await GetQueueClientAsync(cancellationToken);
            await queueClient.SendMessagesAsync(messages, cancellationToken);
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
