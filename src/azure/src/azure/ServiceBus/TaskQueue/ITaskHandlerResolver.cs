using System;
using System.Collections.Generic;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskHandlerResolver {
        IEnumerable<ITaskHandler> ResolveHandlers(Type taskRequestType);
    }
}
