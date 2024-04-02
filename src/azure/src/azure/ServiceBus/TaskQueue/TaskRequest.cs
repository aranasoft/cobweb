using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Aranasoft.Cobweb.Azure.ServiceBus.TaskQueue {
    /// <summary>
    /// Represents a task request with a set of parameters and a unique identifier.
    /// </summary>
    public class TaskRequest {
        /// <summary>
        /// Gets or sets the parameters for the task request. The parameters are stored as key-value pairs.
        /// Keys are case-insensitive.
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; } =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Gets or sets the unique identifier for the task request.
        /// </summary>
        public Guid TrackingId { get; set; }

        /// <summary>
        /// Serializes the task request to a JSON string.
        /// </summary>
        /// <returns>A JSON string that represents the task request.</returns>
        public string ToJson()
            => JsonConvert.SerializeObject(this,
                                           Formatting.None,
                                           new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
    }
}
