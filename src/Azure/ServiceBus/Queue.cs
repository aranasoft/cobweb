using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}
