using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    public class TaskRequest {
        /// <summary>
        /// Property bag to store serialized values
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; } =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        public Guid TrackingId { get; set; }

        public string ToJson()
            => JsonConvert.SerializeObject(this,
                                           Formatting.None,
                                           new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
    }
}
