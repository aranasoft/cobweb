using NHibernate;

namespace Cobweb.Data.NHibernate {
    public class NHibernateTransactionHandler : IDataTransaction {
        private readonly ITransaction _transaction;

        public NHibernateTransactionHandler(ITransaction transaction) {
            _transaction = transaction;
        }

        public bool IsActive {
            get { return _transaction.IsActive; }
        }

        public void Commit() {
            _transaction.Commit();
        }

        public void Rollback() {
            _transaction.Rollback();
        }

        public void Dispose() {
            _transaction.Dispose();
        }
    }
}
