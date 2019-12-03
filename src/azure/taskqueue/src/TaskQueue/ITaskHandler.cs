namespace Cobweb.TaskQueue {
    public interface ITaskHandler {
        TaskRequest Request { get; set; }
        bool HandleTask();
    }
}
