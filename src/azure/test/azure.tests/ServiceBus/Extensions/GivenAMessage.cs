using System;
using Aranasoft.Cobweb.Azure.ServiceBus.Extensions;
using Azure.Messaging.ServiceBus;
using FluentAssertions;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.Extensions {
    public class GivenAMessage {
        [Fact]
        public void ItShouldHaveAFutureEnqueueTime() {
            new ServiceBusMessage().Delay(TimeSpan.FromDays(3))
            .ScheduledEnqueueTime.Should()
            .BeCloseTo(DateTime.UtcNow.AddDays(3), TimeSpan.FromMinutes(1));
        }
    }
}
