using System;
using System.Data;
using Aranasoft.Cobweb.DependencyInjection;

namespace Aranasoft.Cobweb.Data {
    public interface IDataTransactionManager : IDependency {
        IDataTransaction BeginTransaction();
        IDataTransaction BeginTransaction(IsolationLevel isolationLevel);

        void ExecuteTransaction(Action work);

        TEntity ExecuteTransaction<TEntity>(TEntity entity, Action<TEntity> work)
            where TEntity : IEntity<TEntity>, IEquatable<TEntity>;
    }
}
