using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cobweb.Azure.ServiceBus.Extensions;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Cobweb.Azure.ServiceBus {
    public abstract class Queue {
        private NamespaceManager _namespaceManager;

        private QueueClient _queueClient;

        private bool queueValidated;
        protected abstract string Name { get; }
        protected abstract string ConnectionString { get; set; }

        protected NamespaceManager NamespaceManager {
            get {
                _namespaceManager = _namespaceManager ??
                                    (_namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString));
                return _namespaceManager;
            }
        }

        protected QueueClient QueueClient {
            get {
                EnsureQueue();
                _queueClient = _queueClient ??
                               (_queueClient = QueueClient.CreateFromConnectionString(ConnectionString, Name));
                return _queueClient;
            }
        }

        protected void EnsureQueue() {
            if (!NamespaceManager.QueueExists(Name)) {
                NamespaceManager.CreateQueue(Name);
                queueValidated = true;
            }
        }

        public void SendMessage(BrokeredMessage message) {
            QueueClient.Send(message);
        }

        public Task SendMessageAsync(BrokeredMessage message) {
            return QueueClient.SendAsync(message);
        }

        public void SendMessages(IEnumerable<BrokeredMessage> messages) {
            QueueClient.SendBatch(messages);
        }

        public Task SendMessagesAsync(IEnumerable<BrokeredMessage> messages) {
            return QueueClient.SendBatchAsync(messages);
        }

        public void OnMessage(Action<BrokeredMessage> callback) {
            QueueClient.OnMessage(callback);
        }

        public void OnMessage(Action<BrokeredMessage> callback, OnMessageOptions options) {
            QueueClient.OnMessage(callback, options);
        }

        private void SetEnqueueTime(BrokeredMessage message, int delaySeconds) {
            if (delaySeconds == 0) return;

            var now = DateTime.UtcNow;
            var then = now + TimeSpan.FromSeconds(delaySeconds);

            message.ScheduledEnqueueTimeUtc = then;
        }

        private void SetEnqueueTime(IList<BrokeredMessage> messages, int delaySeconds) {
            foreach (var message in messages) {
                message.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }
    }
}
