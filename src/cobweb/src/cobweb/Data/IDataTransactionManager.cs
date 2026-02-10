using System;
using System.Data;
using Aranasoft.Cobweb.DependencyInjection;

namespace Aranasoft.Cobweb.Data;
/// <summary>
///     Provides methods to create and execute data transactions.
/// </summary>
public interface IDataTransactionManager : IDependency {
    /// <summary>
    ///     Begins a new data transaction.
    /// </summary>
    /// <returns>A new <see cref="IDataTransaction" />.</returns>
    IDataTransaction BeginTransaction();

    /// <summary>
    ///     Begins a new data transaction with the specified isolation level.
    /// </summary>
    /// <param name="isolationLevel">The isolation level for the transaction.</param>
    /// <returns>A new <see cref="IDataTransaction" />.</returns>
    IDataTransaction BeginTransaction(IsolationLevel isolationLevel);

    /// <summary>
    ///     Executes the specified work within a transaction, committing on success or rolling back on failure.
    /// </summary>
    /// <param name="work">The action to execute within the transaction.</param>
    void ExecuteTransaction(Action work);

    /// <summary>
    ///     Executes the specified work on an entity within a transaction, committing on success or rolling back on failure.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entity">The entity to operate on.</param>
    /// <param name="work">The action to execute on the entity within the transaction.</param>
    /// <returns>The entity after the transaction has been committed.</returns>
    TEntity ExecuteTransaction<TEntity>(TEntity entity, Action<TEntity> work)
        where TEntity : IEntity<TEntity>, IEquatable<TEntity>;
}
