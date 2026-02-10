using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit;
internal class ConditionalFactAttributeDiscoverer : IXunitTestCaseDiscoverer {
    private readonly IMessageSink _diagnosticMessageSink;

    public ConditionalFactAttributeDiscoverer(IMessageSink diagnosticMessageSink) {
        _diagnosticMessageSink = diagnosticMessageSink;
    }

    public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions,
                                                ITestMethod testMethod,
                                                IAttributeInfo factAttribute) {
        var methodDisplay = discoveryOptions.MethodDisplayOrDefault();
        var methodDisplayOptions = discoveryOptions.MethodDisplayOptionsOrDefault();

        return new[] {
            new ConditionalFactTestCase(_diagnosticMessageSink, methodDisplay, methodDisplayOptions, testMethod)
        };
    }
}
