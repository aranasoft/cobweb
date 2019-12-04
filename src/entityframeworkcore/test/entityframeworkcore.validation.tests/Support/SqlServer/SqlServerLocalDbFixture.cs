using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer {
    [Parallelizable(ParallelScope.All)]
    public class SqlServerLocalDbFixture {
        public ApplicationDbContext GetContext() {
            return new ApplicationDbContext(_builder.Options);
        }

        private LocalDbTestingDatabase _testingDatabase;
        private DbContextOptionsBuilder<ApplicationDbContext> _builder;
        private SqlConnection _dbConnection;

        [OneTimeSetUp]
        public void EstablishTestDatabase() {
            var serviceCollection = new ServiceCollection().AddEntityFrameworkDesignTimeServices();
            new SqlServerDesignTimeServices().ConfigureDesignTimeServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            _testingDatabase = new LocalDbTestingDatabase();
            _testingDatabase.EnsureDatabaseAsync().Wait();

            var connectionString = _testingDatabase.ConnectionString;
            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();

            _builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _builder.UseSqlServer(_dbConnection);
            _builder.UseApplicationServiceProvider(serviceProvider);
        }

        [OneTimeTearDown]
        public void DestroyTestDatabase() {
            if (_dbConnection.State == ConnectionState.Open) _dbConnection.Close();
            _dbConnection.Dispose();

            _testingDatabase.Dispose();
        }
    }
}
