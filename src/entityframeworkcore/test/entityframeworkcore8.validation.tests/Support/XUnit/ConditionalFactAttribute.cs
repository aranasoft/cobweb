using System;
using Xunit;
using Xunit.Sdk;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit {
    [AttributeUsage(AttributeTargets.Method)]
    [XunitTestCaseDiscoverer(
        "Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit.ConditionalFactAttributeDiscoverer",
        "Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests")]
    public class ConditionalFactAttribute : FactAttribute {}
}
