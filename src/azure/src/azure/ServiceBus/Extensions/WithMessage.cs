using System;
using Microsoft.Azure.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.Extensions {
    public static class WithMessage {
        public static Message Delay(this Message message, TimeSpan delay) {
            if (delay <= TimeSpan.Zero) return message;

            var now = DateTime.UtcNow;
            var enqueueTime = now + delay;
            message.ScheduledEnqueueTimeUtc = enqueueTime;

            return message;
        }
    }
}
