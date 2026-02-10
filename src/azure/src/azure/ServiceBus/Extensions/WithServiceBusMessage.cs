using System;
using System.Linq;
using Azure.Messaging.ServiceBus;

namespace Aranasoft.Cobweb.Azure.ServiceBus.Extensions;
/// <summary>
/// Provides extension methods for <see cref="ServiceBusMessage"/>.
/// </summary>
public static class WithServiceBusMessage {
    /// <summary>
    /// Delays the enqueue time of the <see cref="ServiceBusMessage"/> by a specified <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="message">The <see cref="ServiceBusMessage"/> to delay.</param>
    /// <param name="delay">The <see cref="TimeSpan"/> to delay the message by.</param>
    /// <returns>The <see cref="ServiceBusMessage"/> with the updated enqueue time.</returns>
    /// <remarks>
    /// If the delay is less than or equal to <see cref="TimeSpan.Zero"/>, the original message is returned without modification.
    /// The enqueue time is calculated as the current UTC time plus the delay.
    /// </remarks>
    public static ServiceBusMessage Delay(this ServiceBusMessage message, TimeSpan delay) {
        if (delay <= TimeSpan.Zero) return message;

        var now = DateTime.UtcNow;
        var enqueueTime = now + delay;
        message.ScheduledEnqueueTime = enqueueTime;

        return message;
    }
}
