using System;
using System.Data;

namespace Aranasoft.Cobweb.Data {
    /// <summary>
    ///     Abstract base implementation of <see cref="IDataTransactionManager" /> that commits or rolls back
    ///     transactions around a unit of work.
    /// </summary>
    public abstract class DataTransactionManager : IDataTransactionManager {
        /// <inheritdoc />
        public abstract IDataTransaction BeginTransaction();

        /// <inheritdoc />
        public abstract IDataTransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <inheritdoc />
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

        /// <inheritdoc />
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