using System;
using Aranasoft.Cobweb.Azure.ServiceBus.Extensions;
using FluentAssertions;
using Microsoft.Azure.ServiceBus;
using Xunit;

namespace Aranasoft.Cobweb.Azure.Tests.ServiceBus.Extensions {
    public class GivenAMessage {
        [Fact]
        public void ItShouldHaveAFutureEnqueueTime() {
            new Message().Delay(TimeSpan.FromDays(3))
            .ScheduledEnqueueTimeUtc.Should()
            .BeCloseTo(DateTime.UtcNow.AddDays(3), TimeSpan.FromMinutes(1));
        }
    }
}
