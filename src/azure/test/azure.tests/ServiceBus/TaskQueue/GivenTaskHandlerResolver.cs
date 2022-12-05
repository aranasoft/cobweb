using System;
using System.Threading;
using System.Threading.Tasks;
using Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.TaskQueue {
    public class GivenTaskHandlerResolver {
        private readonly TaskHandlerResolver _resolver;

        public GivenTaskHandlerResolver() {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTaskQueue<TestTaskRequestQueue>();
            serviceCollection.AddTaskHandler<TestTaskHandler, TestTaskRequest>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            _resolver = new TaskHandlerResolver(serviceProvider, NullLogger<TaskHandlerResolver>.Instance);
        }

        [Fact]
        public void ItShouldResolveHandlerForHandledRequestType() {
            var handlers = _resolver.ResolveHandlers(typeof(TestTaskRequest));
            handlers.Should().HaveCount(1).And.ContainItemsAssignableTo<TestTaskHandler>();
        }

        [Fact]
        public void ItShouldNotResolveHandlerForUnhandledRequestType() {
            var handlers = _resolver.ResolveHandlers(typeof(UnhandledTestTaskRequest));
            handlers.Should().BeEmpty();
        }

        public class TestTaskHandler : TaskHandler<TestTaskRequest> {
            protected override Task<bool> ExecuteAsync(
            TestTaskRequest taskRequest,
            CancellationToken cancellationToken = default) {
                throw new NotImplementedException();
            }
        }

        public class TestTaskRequestQueue : TaskRequestQueue {
            public TestTaskRequestQueue(
            ServiceBusAdministrationClient managementClient,
            ServiceBusClient queueClient,
            string taskQueueName) : base(managementClient, queueClient, taskQueueName) {}
        }

        public class TestTaskRequest : TaskRequest {}

        public class UnhandledTestTaskRequest : TaskRequest {}
    }
}
