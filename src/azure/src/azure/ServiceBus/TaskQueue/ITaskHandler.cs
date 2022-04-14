using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskHandler {
        TaskRequest Request { get; set; }
        Task<bool> HandleTaskAsync(CancellationToken cancellationToken = default);
    }
}
