using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskProcessor {
        Task ProcessQueueMessageAsync(string message, CancellationToken cancellationToken = default);
    }
}
