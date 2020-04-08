using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit {
    public class ConditionalFactTestCase : XunitTestCase {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete( "Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        // ReSharper disable once UnusedMember.Global
        public ConditionalFactTestCase() : base() {}

        public ConditionalFactTestCase(
            IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay,
            TestMethodDisplayOptions defaultMethodDisplayOptions,
            ITestMethod testMethod,
            object[] testMethodArguments = null)
            : base(diagnosticMessageSink,
                   defaultMethodDisplay,
                   defaultMethodDisplayOptions,
                   testMethod,
                   testMethodArguments) {}

        public override async Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
                                                        IMessageBus messageBus,
                                                        object[] constructorArguments,
                                                        ExceptionAggregator aggregator,
                                                        CancellationTokenSource cancellationTokenSource) {
            return await IsSatisfiedAsync(messageBus)
                ? await base.RunAsync(diagnosticMessageSink,
                                      messageBus,
                                      constructorArguments,
                                      aggregator,
                                      cancellationTokenSource)
                : new RunSummary {Skipped = 1, Total = 1};
        }

        public async Task<bool> IsSatisfiedAsync(IMessageBus messageBus) {
            var requirements = GetTestRequirements().ToList();

            var skipReasons = new List<string>();

            foreach (var requirement in requirements) {
                if (!await requirement.IsSatisfiedAsync()) {
                    skipReasons.Add(requirement.SkipReason);
                }
            }

            if (!skipReasons.Any()) {
                return true;
            }

            messageBus.QueueMessage(
                new TestSkipped(new XunitTest(this, DisplayName), string.Join(Environment.NewLine, skipReasons)));
            return false;
        }

        private IEnumerable<ITestCaseRequirementAttribute> GetTestRequirements() {
            var testClass = TestMethod.TestClass.Class;
            var testAssembly = testClass.Assembly;
            var requirements = Method.GetCustomAttributes(typeof(ITestCaseRequirementAttribute))
                                     .Concat(testClass.GetCustomAttributes(typeof(ITestCaseRequirementAttribute)))
                                     .Concat(testAssembly.GetCustomAttributes(typeof(ITestCaseRequirementAttribute)))
                                     .OfType<ReflectionAttributeInfo>()
                                     .Select(attributeInfo => (ITestCaseRequirementAttribute) attributeInfo.Attribute);
            return requirements;
        }
    }
}
