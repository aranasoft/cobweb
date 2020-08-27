using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskProcessor {
        Task ProcessQueueMessage(string message);
    }
}