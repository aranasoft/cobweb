using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Cobweb.Azure.Storage {
    public abstract class Table {
        private CloudStorageAccount _account;

        private CloudTableClient _client;

        private CloudTable _tableReference;
        public abstract string Name { get; set; }
        protected abstract string ConnectionString { get; set; }

        protected CloudStorageAccount Account {
            get {
                _account = _account ?? CloudStorageAccount.Parse(ConnectionString);
                return _account;
            }
        }

        protected CloudTableClient Client {
            get {
                _client = _client ?? Account.CreateCloudTableClient();
                return _client;
            }
            set { _client = value; }
        }

        protected CloudTable TableReference {
            get {
                _tableReference = _tableReference ?? Client.GetTableReference(Name);
                return _tableReference;
            }
            set { _tableReference = value; }
        }

        protected void EnsureTable() {
            TableReference.CreateIfNotExists();
        }
    }
}
