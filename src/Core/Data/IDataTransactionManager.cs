using System.Data;
using Cobweb.DependencyInjection;

namespace Cobweb.Data {
    public interface IDataTransactionManager : IDependency {
        IDataTransaction BeginTransaction();
        IDataTransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
