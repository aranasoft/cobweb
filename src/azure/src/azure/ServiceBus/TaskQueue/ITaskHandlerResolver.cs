using System.Collections.Generic;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskHandlerResolver {
        IEnumerable<ITaskHandler> ResolveHandlers(TaskRequest taskRequest);
    }
}
