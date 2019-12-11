using System.Threading.Tasks;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit {
    public interface ITestCaseRequirementAttribute {
        Task<bool> IsSatisfiedAsync();

        string SkipReason { get; }
    }
}
