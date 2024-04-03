# Aranasoft.Cobweb.Azure
![Azure DevOps](https://dev.azure.com/aranasoft/Cobweb/_apis/build/status/Aranasoft.Cobweb.Azure?branchName=master)

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

## License

Cobweb is copyright of Arana Software, released under the [BSD License](http://opensource.org/licenses/BSD-3-Clause).

