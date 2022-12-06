using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskRequestQueue {
        Task AddTaskAsync(TaskRequest taskRequest, CancellationToken cancellationToken = default);
        Task ScheduleTaskAsync(TaskRequest request, DateTimeOffset scheduledMessageEnqueueTime, CancellationToken cancellationToken = default);
    }
}
