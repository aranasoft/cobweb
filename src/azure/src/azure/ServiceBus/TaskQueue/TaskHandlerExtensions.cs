using System;
using System.Linq;
using Aranasoft.Cobweb.Extensions;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public static class TaskHandlerExtensions {
        public static bool IsTaskHandler(this Type handlerType) {
            return handlerType.IsClass &&
                   !handlerType.IsAbstract &&
                   handlerType.BaseType?.IsAssignableToGeneric(typeof(TaskHandler<>)) == true;
        }

        public static Type GetHandledRequestType(this Type handlerType) {
            if (!handlerType.IsAssignableToGeneric(typeof(TaskHandler<>))) {
                throw new ArgumentException($"Type is not assignable to {typeof(TaskHandler<>).Name}", nameof(handlerType));
            }

            var handlerBaseType = handlerType.GetGenericParentType(typeof(TaskHandler<>));
            var requestType = handlerBaseType.GenericTypeArguments.First();

            return requestType;
        }

        public static bool HandlesRequest(this Type handlerType, Type taskRequestType) {
            var handledRequestType = handlerType.GetHandledRequestType();

            return taskRequestType.IsAssignableTo(handledRequestType);
        }
    }
}
