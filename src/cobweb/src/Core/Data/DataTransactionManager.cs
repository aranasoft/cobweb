using System;
using System.Data;

namespace Cobweb.Data {
    public abstract class DataTransactionManager : IDataTransactionManager {
        public abstract IDataTransaction BeginTransaction();
        public abstract IDataTransaction BeginTransaction(IsolationLevel isolationLevel);

        public virtual void ExecuteTransaction(Action work) {
            using (IDataTransaction tx = BeginTransaction()) {
                try {
                    work.Invoke();
                    tx.Commit();
                }
                catch (Exception) {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public virtual TEntity ExecuteTransaction<TEntity>(TEntity entity, Action<TEntity> work)
            where TEntity : IEntity<TEntity>, IEquatable<TEntity> {
            using (IDataTransaction tx = BeginTransaction()) {
                try {
                    work.Invoke(entity);
                    tx.Commit();
                    return entity;
                }
                catch (Exception) {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}