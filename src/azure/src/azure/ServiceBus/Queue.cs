using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace Aranasoft.Cobweb.Azure.ServiceBus {
    public abstract class Queue {
        private readonly ServiceBusAdministrationClient _managementClient;
        private readonly ServiceBusClient _queueClient;
        private ServiceBusSender _queueClientSender;
        private ServiceBusProcessor _queueProcessor;
        private bool _queueValidated;

        protected string Name { get; }

        protected Queue(ServiceBusAdministrationClient managementClient, ServiceBusClient queueClient, string queueName) {
            _managementClient = managementClient;
            _queueClient = queueClient;
            Name = queueName ?? throw new ArgumentNullException(nameof(queueName));
        }

        protected async Task<ServiceBusSender> GetQueueSenderAsync(CancellationToken cancellationToken = default) {
            await EnsureQueueAsync(cancellationToken);
            return _queueClientSender ??= _queueClient.CreateSender(Name);
        }

        protected async Task<ServiceBusProcessor> GetQueueProcessorAsync(CancellationToken cancellationToken = default) {
            await EnsureQueueAsync(cancellationToken);
            _queueProcessor ??= _queueClient.CreateProcessor(Name);
            return _queueProcessor;
        }


        protected async Task EnsureQueueAsync(CancellationToken cancellationToken = default) {
            if (!_queueValidated && !await _managementClient.QueueExistsAsync(Name, cancellationToken)) {
                await _managementClient.CreateQueueAsync(Name, cancellationToken);
                _queueValidated = true;
            }
        }

        /// <summary>
        /// Sends a message to Service Bus.
        /// </summary>
        public async Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken = default) {
            var queueClient = await GetQueueSenderAsync(cancellationToken);
            await queueClient.SendMessageAsync(message, cancellationToken);
        }

        /// <summary>
        /// Sends a list of messages to Service Bus.
        /// </summary>
        public async Task SendMessagesAsync(IEnumerable<ServiceBusMessage> messages, CancellationToken cancellationToken = default) {
            var queueClient = await GetQueueSenderAsync(cancellationToken);
            await queueClient.SendMessagesAsync(messages, cancellationToken);
        }

        public async Task RegisterErrorHandlerAsync(Func<ProcessErrorEventArgs, Task> exceptionCallback) {
            var processor = await GetQueueProcessorAsync();
            processor.ProcessErrorAsync += exceptionCallback;
        }

        public async Task RegisterMessageHandlerAsync(Func<ProcessMessageEventArgs, Task> callback) {
            var processorAsync = await GetQueueProcessorAsync();
            processorAsync.ProcessMessageAsync += callback;
        }
    }
}
