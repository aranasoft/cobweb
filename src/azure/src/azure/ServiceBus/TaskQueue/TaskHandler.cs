using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public abstract class TaskHandler<TTaskRequest> : ITaskHandler where TTaskRequest : TaskRequest {
        public Type GetHandledRequestType() {
            return typeof(TTaskRequest);
        }

        protected virtual Task<bool> BeforeExecuteAsync(TTaskRequest taskRequest, CancellationToken cancellationToken = default) {
            return Task.FromResult(true);
        }

        protected abstract Task<bool> ExecuteAsync(TTaskRequest taskRequest, CancellationToken cancellationToken = default);

        protected virtual Task<bool> AfterExecuteAsync(TTaskRequest taskRequest, CancellationToken cancellationToken = default) {
            return Task.FromResult(true);
        }

        public virtual async Task<bool> HandleTaskAsync(TaskRequest taskRequest, CancellationToken cancellationToken = default) {
            if (taskRequest == null) throw new ArgumentNullException(nameof(taskRequest));
            if (!(taskRequest is TTaskRequest castTaskRequest)) throw new ArgumentException($"TaskRequest must be of type {typeof(TTaskRequest)}", nameof(taskRequest));

            var successful = await BeforeExecuteAsync(castTaskRequest, cancellationToken);
            if (successful) {
                successful = await ExecuteAsync(castTaskRequest, cancellationToken);
            }

            if (successful) {
                successful = await AfterExecuteAsync(castTaskRequest, cancellationToken);
            }

            return successful;
        }
    }
}
