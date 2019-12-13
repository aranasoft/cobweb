using System;
using System.Collections.Generic;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskRequest {
        public string Name { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public Guid TrackingId { get; set; }

        protected TaskRequest() {
            Parameters = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
