using System.Threading.Tasks;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public abstract class TaskHandler : ITaskHandler {
        public TaskRequest Request { get; set; }

        protected virtual Task<bool> BeforeExecuteAsync() {
            return Task.FromResult(true);
        }

        protected abstract Task<bool> ExecuteAsync();

        protected virtual Task<bool> AfterExecuteAsync() {
            return Task.FromResult(true);
        }

        public virtual async Task<bool> HandleTaskAsync() {
            var successful = await BeforeExecuteAsync();
            if (successful) {
                successful = await ExecuteAsync();
            }

            if (successful) {
                successful = await AfterExecuteAsync();
            }

            return successful;
        }
    }
}
