using System;
using System.Collections.Generic;

namespace Cobweb.TaskQueue {
    public class TaskRequest {
        public string Name { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public Guid TrackingId { get; set; }

        protected TaskRequest() {
            Parameters = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
