using System;
using System.Collections.Generic;
using Aranasoft.Cobweb.Reflection.Extensions;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskRequest {
        protected TaskRequest() {
            var myType = GetType();
            if (myType != typeof(TaskRequest)) {
                Name = myType.GetCustomAttribute<TaskRequestNameAttribute>()?.Name ??
                       throw new TaskRequestException($"Task Request {myType.FullName} is missing a TaskRequestNameAttribute");
            }

            Parameters = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public string Name { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public Guid TrackingId { get; set; }
    }
}
