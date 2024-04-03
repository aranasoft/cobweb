using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    /// <summary>
    /// Provides an abstract base class for task handlers.
    /// </summary>
    /// <typeparam name="TTaskRequest">The type of the task request to handle. This type must derive from <see cref="TaskRequest"/>.</typeparam>
    public abstract class TaskHandler<TTaskRequest> : ITaskHandler where TTaskRequest : TaskRequest {
        /// <inheritdoc />
        public Type GetHandledRequestType() {
            return typeof(TTaskRequest);
        }

        /// <summary>
        /// Executes before the main execution of the task. This method can be overridden in derived classes.
        /// </summary>
        /// <param name="taskRequest">The <typeparamref name="TTaskRequest"/> to handle.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        protected virtual Task<bool> BeforeExecuteAsync(TTaskRequest taskRequest, CancellationToken cancellationToken = default) {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Executes the main logic of the task. This method must be overridden in derived classes.
        /// </summary>
        /// <param name="taskRequest">The <typeparamref name="TTaskRequest"/> to handle.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        protected abstract Task<bool> ExecuteAsync(TTaskRequest taskRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes after the main execution of the task. This method can be overridden in derived classes.
        /// </summary>
        /// <param name="taskRequest">The <typeparamref name="TTaskRequest"/> to handle.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        protected virtual Task<bool> AfterExecuteAsync(TTaskRequest taskRequest, CancellationToken cancellationToken = default) {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
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
