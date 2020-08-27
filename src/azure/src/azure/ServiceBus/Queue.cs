using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.Configuration;
using Aranasoft.Cobweb.Azure.ServiceBus.Extensions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;

namespace Aranasoft.Cobweb.Azure.ServiceBus {
    public abstract class Queue {
        private ManagementClient _managementClient;
        private QueueClient _queueClient;
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

        protected ManagementClient ManagementClient {
            get {
                _managementClient = _managementClient ??
                                    (_managementClient = new ManagementClient(ConnectionString));
                return _managementClient;
            }
        }

        protected async Task<QueueClient> GetQueueClientAsync() {
            await EnsureQueueAsync();
            _queueClient = _queueClient ??
                           (_queueClient = new QueueClient(ConnectionString, Name));
            return _queueClient;
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
        public async Task SendMessageAsync(Message message) {
            var queueClient = await GetQueueClientAsync();
            await queueClient.SendAsync(message);
        }

        /// <summary>
        /// Sends a list of messages to Service Bus.
        /// </summary>
        public async Task SendMessagesAsync(IList<Message> messages) {
            var queueClient = await GetQueueClientAsync();
            await queueClient.SendAsync(messages);
        }

        public async Task RegisterMessageHandlerAsync(Func<Message, CancellationToken, Task> callback,
                                                      Func<ExceptionReceivedEventArgs, Task> exceptionCallback) {
            var queueClient = await GetQueueClientAsync();
            queueClient.RegisterMessageHandler(callback, exceptionCallback);
        }

        public async Task RegisterMessageHandlerAsync(Func<Message, CancellationToken, Task> callback,
                                                      MessageHandlerOptions options) {
            var queueClient = await GetQueueClientAsync();
            queueClient.RegisterMessageHandler(callback, options);
        }

        private void SetEnqueueTime(Message message, int delaySeconds) {
            message.Delay(TimeSpan.FromSeconds(delaySeconds));
        }

        private void SetEnqueueTime(IList<Message> messages, int delaySeconds) {
            foreach (var message in messages) {
                message.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }
    }
}
