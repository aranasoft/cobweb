using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer {
    public class SqlServerLocalDbFixture : IDisposable {
        public ApplicationDbContext GetContext() {
            return new ApplicationDbContext(_builder.Options);
        }

        private readonly LocalDbTestingDatabase _testingDatabase;
        private readonly DbContextOptionsBuilder<ApplicationDbContext> _builder;
        private readonly SqlConnection _dbConnection;

        public SqlServerLocalDbFixture() {
            var serviceCollection = new ServiceCollection().AddEntityFrameworkDesignTimeServices();
            new SqlServerDesignTimeServices().ConfigureDesignTimeServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            _testingDatabase = new LocalDbTestingDatabase();
            _testingDatabase.EnsureDatabase();

            var connectionString = _testingDatabase.ConnectionString;
            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();

            _builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _builder.UseSqlServer(_dbConnection);
            _builder.UseApplicationServiceProvider(serviceProvider);
        }

        public virtual void Dispose() {
            if (_dbConnection.State == ConnectionState.Open) _dbConnection.Close();
            _dbConnection.Dispose();

            _testingDatabase.Dispose();
        }
    }
}
