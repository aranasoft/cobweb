namespace Cobweb.TaskQueue {
    public abstract class TaskHandler : ITaskHandler {
        public TaskRequest Request { get; set; }

        protected virtual bool BeforeExecute() {
            return true;
        }

        protected abstract bool Execute();

        protected virtual bool AfterExecute() {
            return true;
        }

        public virtual bool HandleTask() {
            var successful = BeforeExecute();
            if (successful) {
                successful = Execute();
            }

            if (successful) {
                successful = AfterExecute();
            }

            return successful;
        }
    }
}
