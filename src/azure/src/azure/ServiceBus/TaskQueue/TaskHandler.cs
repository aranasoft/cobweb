using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public abstract class TaskHandler : ITaskHandler {
        public TaskRequest Request { get; set; }

        protected virtual Task<bool> BeforeExecuteAsync(CancellationToken cancellationToken = default) {
            return Task.FromResult(true);
        }

        protected abstract Task<bool> ExecuteAsync(CancellationToken cancellationToken = default);

        protected virtual Task<bool> AfterExecuteAsync(CancellationToken cancellationToken = default) {
            return Task.FromResult(true);
        }

        public virtual async Task<bool> HandleTaskAsync(CancellationToken cancellationToken = default) {
            var successful = await BeforeExecuteAsync(cancellationToken);
            if (successful) {
                successful = await ExecuteAsync(cancellationToken);
            }

            if (successful) {
                successful = await AfterExecuteAsync(cancellationToken);
            }

            return successful;
        }
    }
}
