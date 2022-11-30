using Azure.Data.Tables;

namespace Aranasoft.Cobweb.Azure.Storage {
    public abstract class Table {
        private TableServiceClient _client;

        private TableClient _tableReference;
        public abstract string Name { get; set; }
        protected abstract string ConnectionString { get; set; }

        protected TableServiceClient ServiceClient {
            get {
                _client = _client ?? new TableServiceClient(ConnectionString);
                return _client;
            }
            set { _client = value; }
        }

        protected TableClient GetTableClient {
            get {
                _tableReference = _tableReference ?? ServiceClient.GetTableClient(Name);
                return _tableReference;
            }
            set { _tableReference = value; }
        }

        protected void EnsureTable() {
            GetTableClient.CreateIfNotExists();
        }
    }
}
