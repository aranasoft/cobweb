using System;
using System.Linq;
using Azure.Messaging.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.Extensions {
    public static class WithServiceBusMessage {
        public static ServiceBusMessage Delay(this ServiceBusMessage message, TimeSpan delay) {
            if (delay <= TimeSpan.Zero) return message;

            var now = DateTime.UtcNow;
            var enqueueTime = now + delay;
            message.ScheduledEnqueueTime = enqueueTime;

            return message;
        }
    }
}
