using System.Data;
using Aranasoft.Cobweb.DependencyInjection;

namespace Aranasoft.Cobweb.Data {
    public interface IDataTransactionManager : IDependency {
        IDataTransaction BeginTransaction();
        IDataTransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}