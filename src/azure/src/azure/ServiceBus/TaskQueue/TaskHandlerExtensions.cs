using System;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public static class TaskHandlerExtensions {
        private static bool IsTaskHandler(Type handlerType) {
            return handlerType.IsClass &&
                   !handlerType.IsAbstract &&
                   handlerType.GetInterfaces().Any(typeInterfaces => typeInterfaces == typeof(ITaskHandler));
        }

        public static bool HandlesRequests(this Type handlerType) {
            var handledRequestsField = handlerType.GetCustomAttribute<TaskHandlesRequestAttribute>();
            return handledRequestsField != null && handledRequestsField.RequestTypes.Any();
        }

        public static bool HandlesRequest(this Type handlerType, TaskRequest taskRequest) {
            var taskRequestName = taskRequest.Name;

            var handledRequestsField = handlerType.GetCustomAttribute<TaskHandlesRequestAttribute>();
            if (handledRequestsField == null) {
                return false;
            }

            return handledRequestsField.RequestTypes.Any(handledRequest => {
                var handledName = handledRequest.GetCustomAttribute<TaskRequestNameAttribute>()?.Name;
                return string.Equals(handledName, taskRequestName);
            });
        }

        public static IServiceCollection AddTaskHandlers(this IServiceCollection services, Assembly assembly) {
            var currentServices = services;
            currentServices = AddTaskHandlerServices(currentServices);
            return AddTaskHandlersFromAssembly(currentServices, assembly);
        }

        public static IServiceCollection AddTaskHandlers(this IServiceCollection services, Assembly[] assemblies) {
            var currentServices = services;
            currentServices = AddTaskHandlerServices(currentServices);
            return assemblies.Aggregate(currentServices, AddTaskHandlersFromAssembly);
        }

        private static IServiceCollection AddTaskHandlerServices(IServiceCollection services) {
            var currentServices = services;
            currentServices = currentServices.AddTransient<ITaskHandlerResolver, TaskHandlerResolver>();
            return currentServices;
        }

        private static IServiceCollection AddTaskHandlersFromAssembly(IServiceCollection services, Assembly assembly) {
            var handlerTypes = assembly.GetTypes().Where(IsTaskHandler).Where(HandlesRequests).ToList();

            var currentServices = services;
            foreach (var type in handlerTypes) {
                currentServices = currentServices.AddTransient(type);
                currentServices = currentServices.AddTransient(typeof(ITaskHandler), type);
                currentServices = currentServices.AddTransient(typeof(TaskHandler), type);
            }

            currentServices.AddTransient(provider => handlerTypes
                                         .ToDictionary(type => type,
                                                       type => new Lazy<TaskHandler>(
                                                       () => provider.GetService(type) as TaskHandler)));
            currentServices.AddTransient(provider => handlerTypes
                                         .ToDictionary(type => type,
                                                       type => new Lazy<ITaskHandler>(
                                                       () => provider.GetService(type) as ITaskHandler)));

            return currentServices;
        }
    }
}
