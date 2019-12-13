namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public interface ITaskHandler {
        TaskRequest Request { get; set; }
        bool HandleTask();
    }
}
