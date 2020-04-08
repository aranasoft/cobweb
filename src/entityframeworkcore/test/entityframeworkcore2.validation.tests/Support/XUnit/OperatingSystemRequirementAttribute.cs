using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class OperatingSystemRequirementAttribute : Attribute, ITestCaseRequirementAttribute {
        public OperatingSystems OperatingSystems { get; set; }
        public bool Exclude { get; set; } = false;
        private readonly OperatingSystems _currentOperatingSystem;

        public OperatingSystemRequirementAttribute(OperatingSystems operatingSystems, bool exclude = false) {
            OperatingSystems = operatingSystems;
            Exclude = exclude;
            _currentOperatingSystem = GetCurrentOperatingSystem();
        }

        public string SkipReason { get; set; } = $"Test not supported on {RuntimeInformation.OSDescription.Trim()}.";

        public Task<bool> IsSatisfiedAsync() {
            var result = OperatingSystems.HasFlag(_currentOperatingSystem) ? !Exclude : Exclude;

            return Task.FromResult(result);
        }

        private static OperatingSystems GetCurrentOperatingSystem() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                return OperatingSystems.Windows;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                return OperatingSystems.MacOS;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                return OperatingSystems.Linux;
            }

            throw new PlatformNotSupportedException();
        }
    }
}
