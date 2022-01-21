using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer {
    class LocalDbTestingDatabase : IDisposable {
        public string LocalDbConnectionString { get; set; }
        public string DatabaseName { get; }

        public LocalDbTestingDatabase() : this($"Test{new Random().Next()}") {}

        public LocalDbTestingDatabase(string databaseName,
                                      string localDbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB") {
            DatabaseName = databaseName;
            LocalDbConnectionString = localDbConnectionString;
        }

        public async Task EnsureDatabaseAsync(CancellationToken cancellationToken = default) {
            using (var connection = new SqlConnection(LocalDbConnectionString)) {
                await connection.OpenAsync(cancellationToken);
                var cmd = connection.CreateCommand();
                cmd.CommandText =
                    $"CREATE DATABASE {DatabaseName} ON PRIMARY ( NAME={DatabaseName}_Data, FILENAME = '{GetDataFilePath()}' ) LOG ON ( NAME={DatabaseName}_Log, FILENAME = '{GetLogFilePath()}' )";
                await cmd.ExecuteNonQueryAsync(cancellationToken);
                connection.Close();
            }

            if (!File.Exists(GetDataFilePath()))
                throw new Exception($"Failed to create database file: {GetDataFilePath()}");
        }

        public void EnsureDatabase() {
            using (var connection = new SqlConnection(LocalDbConnectionString)) {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText =
                    $"CREATE DATABASE {DatabaseName} ON PRIMARY ( NAME={DatabaseName}_Data, FILENAME = '{GetDataFilePath()}' ) LOG ON ( NAME={DatabaseName}_Log, FILENAME = '{GetLogFilePath()}' )";
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            if (!File.Exists(GetDataFilePath()))
                throw new Exception($"Failed to create database file: {GetDataFilePath()}");
        }

        public string ConnectionString {
            get {
                return
                    $"{LocalDbConnectionString};Initial Catalog={DatabaseName};Integrated Security=True; MultipleActiveResultSets=True;AttachDBFilename={GetDataFilePath()}";
            }
        }

        private void DeleteIfExists(string path) {
            if (File.Exists(path)) File.Delete(path);
        }

        public void DeleteDatabase() {
            DeleteIfExists(GetDataFilePath());
            DeleteIfExists(GetLogFilePath());
        }

        public async Task DetachDatabaseAsync(CancellationToken cancellationToken = default) {
            using (var connection = new SqlConnection(LocalDbConnectionString)) {
                await connection.OpenAsync(cancellationToken);
                var cmd = connection.CreateCommand();
                cmd.CommandText =
                    $"ALTER DATABASE [{DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; exec sp_detach_db N'{DatabaseName}'";
                await cmd.ExecuteNonQueryAsync(cancellationToken);
                connection.Close();
            }
        }

        public void DetachDatabase() {
            using (var connection = new SqlConnection(LocalDbConnectionString)) {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText =
                    $"ALTER DATABASE [{DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; exec sp_detach_db N'{DatabaseName}'";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        private string GetDataFilePath() {
            return Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                $"{DatabaseName}.mdf");
        }

        private string GetLogFilePath() {
            return Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                $"{DatabaseName}_Log.ldf");
        }

        public void Dispose() {
            DetachDatabase();
            DeleteDatabase();
        }
    }
}
