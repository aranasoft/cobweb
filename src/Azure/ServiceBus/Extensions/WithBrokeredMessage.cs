using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Cobweb.Azure.ServiceBus.Extensions {
    public static class WithBrokeredMessage {
        public static BrokeredMessage Delay(this BrokeredMessage message, TimeSpan delay) {
            if (delay <= TimeSpan.Zero) return message;

            var now = DateTime.UtcNow;
            var enqueueTime = now + delay;
            message.ScheduledEnqueueTimeUtc = enqueueTime;

            return message;
        }
    }
}
