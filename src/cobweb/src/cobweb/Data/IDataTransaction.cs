using System;

namespace Aranasoft.Cobweb.Data {
    /// <summary>
    ///     Represents a unit-of-work data transaction that can be committed or rolled back.
    /// </summary>
    public interface IDataTransaction : IDisposable {
        /// <summary>
        ///     Gets a value indicating whether the transaction is currently active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        ///     Commits all changes made within the transaction.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Rolls back all changes made within the transaction.
        /// </summary>
        void Rollback();
    }
}
