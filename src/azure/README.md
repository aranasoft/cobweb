# Aranasoft.Cobweb.Azure
[![Azure Utilities](https://github.com/aranasoft/cobweb/actions/workflows/ci-azure.yml/badge.svg)](https://github.com/aranasoft/cobweb/actions/workflows/ci-azure.yml)

`Aranasoft.Cobweb.Azure` is a utility library for Azure. It provides a set of tools and classes for working with Azure services, with a focus on Azure Service Bus and task queue management. The library is designed to be used with .NET and supports multiple target frameworks.

Cobweb was created by [Arana Software](https://www.aranasoft.com), a software agency in Las Vegas, Nevada.

## Details

The library includes a task queue system that uses Azure Service Bus for message transport. It provides a set of classes for creating, managing, and processing tasks in a queue. The task queue system supports task handlers, which are classes that handle specific types of tasks. Task handlers can be registered with the task queue system, and they will be automatically invoked when a task of the corresponding type is received.

## Installation

### Aranasoft.Cobweb.Azure

To install Aranasoft.Cobweb.Azure, add a reference to the package in your project file.

```bash
dotnet add package Aranasoft.Cobweb.Azure
```

Or from NuGet Package Manager:

```bash
Install-Package Aranasoft.Cobweb.Azure
```

## Task Queue

The task queue system is a message-based system that allows tasks to be added to a queue and processed by one or more handlers. The task queue system uses Azure Service Bus for message transport and provides a set of classes for creating, managing, and processing tasks in a queue. The task queue system supports task handlers, which are classes that handle specific types of tasks. Task handlers can be registered with the task queue system, and they will be automatically invoked when a task of the corresponding type is received.

The task queue system can be used for a variety of purposes, such as:

- Processing background tasks
- Scheduling tasks to run at a specific time
- Distributing work across multiple instances of an application
### Basic Usage

A basic example of how to use the task queue system:

```csharp
// Create a new task request
var taskRequest = new TaskRequest {
    Parameters = new Dictionary<string, string> {
        { "param1", "value1" },
        { "param2", "value2" }
    },
    TrackingId = Guid.NewGuid()
};

// Add the task request to the queue
var taskQueue = new TaskRequestQueue(serviceBusClient, "myQueue");
await taskQueue.AddTaskAsync(taskRequest);

// Process messages from the queue
var taskCoordinator = new TaskCoordinator(taskHandlerResolver, logger);
await taskCoordinator.ProcessQueueMessageAsync(message);
```

In this example, `serviceBusClient` is an instance of `ServiceBusClient`, `taskHandlerResolver` is an instance of `ITaskHandlerResolver`, and logger is an instance of `ILogger<TaskCoordinator>`. These instances would typically be provided by your application's dependency injection system.

### Configuring Dependency Injection

The task queue system can be configured with a dependency injection container by building on top of `Microsoft.Extensions.Azure`. The following example shows how to configure the task queue system with a `ServiceCollection`:

```csharp
services.AddAzureClients(clientsBuilder => {
   clientsBuilder.AddTaskQueue(myServiceBusConnectionString)
       .ConfigureOptions(options => {
           busOptions.Identifier = "My Display Name";
           busOptions.QueueName = "myServiceBusQueue";
       });
);
```

This code configures the task queue system to use a Service Bus queue with the specified connection string and queue name. The `Identifier` property is used to identify the queue in logs and error messages.

The task queue system can be resolved from the dependency injection container using the `ITaskRequestQueue` interface:

```csharp
public class MyService {
    private readonly ITaskRequestQueue _taskQueue;

    public MyService(ITaskRequestQueue taskQueue) {
        _taskQueue = taskQueue;
    }

    public async Task AddTaskAsync(TaskRequest taskRequest) {
        await _taskQueue.AddTaskAsync(taskRequest);
    }
}
```

### Task Requests

Task requests are messages that are added to the task queue. They contain a set of parameters that define the task to be performed. Task requests are represented by the `TaskRequest` class. The `TaskRequest` class has two properties: `Parameters` and `TrackingId`. The `Parameters` property is a dictionary of key-value pairs that define the task parameters. The `TrackingId` property is a unique identifier for the task request.

Task requests can be extended to include additional properties by creating a subclass of `TaskRequest`, and saving the additional properties in the `Parameters` dictionary.

```csharp
public class MyTaskRequest : TaskRequest {
    [JsonIgnore]
    public string MyProperty {
        get => Parameters["MyProperty"];
        set => Parameters["MyProperty"] = value;
    }
}
```

### Task Handlers

Task handlers are classes that process task requests of a specific type. They implement the `TaskHandler<TRequest>` base class, where `TRequest` is a specific, derived type `TaskRequest`. A task handler also implements a single method `HandleAsync`, that processes the task request. Task handlers are registered with the task queue system using the `ITaskHandlerResolver` interface:

```csharp
public class MyTaskHandler : TaskHandler<MyTaskRequest> {
    public async Task HandleAsync(MyTaskRequest taskRequest) {
        // Process the task request
    }
}
```

Task handlers are resolved from the dependency injection container based on the type of the task request. Multiple task handlers can be registered for the same task request type, and they will all be invoked when a task request of that type is received.

Task handlers are registered with the service collection using the `AddTaskHandler` extension method:

```csharp
services.AddTaskHandler<MyTaskHandler, MyTaskRequest>();
```

Or by using the type of the task request:

```csharp
services.AddTaskHandler(typeof(MyTaskHandler));
```

Additional registrations allow automatic registration of all task handlers in an assembly:

```csharp
services.AddTaskHandlers(Assembly.GetExecutingAssembly());
```

### Task Coordinator

The `TaskCoordinator` class is responsible for processing messages that are read from the task queue, such as a `ServiceBusTrigger` in an Azure Function. The `TaskCoordinator` class is responsible for resolving the appropriate task handlers for the task request and invoking the `HandleAsync` method for each task handler.

```csharp
// Using a `ITaskCoordinator` instance resolved in a constructor from the service collection
_taskCoordinator.ProcessQueueMessageAsync(serviceBusMessageAsString);
```

The `TaskCoordinator` class can be used in an Azure Function by creating a function that reads messages from the task queue and passes them to the `TaskCoordinator`:

```csharp
[FunctionName("ProcessTaskQueueMessage")]
public async Task Run([ServiceBusTrigger("myServiceBusQueue", Connection = "ServiceBusConnectionString")] string message) {
    await _taskCoordinator.ProcessQueueMessageAsync(message);
}
```

The task coordinator is registered with the service collection using the `AddTaskQueueProcessorServices` extension method:

```csharp
services.AddTaskQueueProcessorServices();
```

### Configuring Dependency Injection for Queue Processing

Configuring the task coordinator and all task handlers can be simplified by using the `AddTaskQueueProcessing` extension method. This method registers all the necessary services for processing task requests in the service collection and all task handlers in the assembly.

```csharp
services.AddTaskQueueProcessing(Assembly.GetExecutingAssembly());
```

## License

Cobweb is copyright of Arana Software, released under the [BSD License](http://opensource.org/licenses/BSD-3-Clause).

