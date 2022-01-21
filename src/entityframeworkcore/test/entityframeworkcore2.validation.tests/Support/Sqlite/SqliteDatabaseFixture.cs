using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite.Design.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite {
    public class SqliteDatabaseFixture : IDisposable {
        public ApplicationDbContext GetContext() {
            return new ApplicationDbContext(_builder.Options);
        }

        private readonly DbContextOptionsBuilder<ApplicationDbContext> _builder;
        private readonly SqliteConnection _dbConnection;

        public SqliteDatabaseFixture() {
            var serviceCollection = new ServiceCollection().AddEntityFrameworkDesignTimeServices();
            new SqliteDesignTimeServices().ConfigureDesignTimeServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            _dbConnection = new SqliteConnection(connectionString);
            _dbConnection.Open();

            _builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _builder.UseSqlite(_dbConnection);
            _builder.UseApplicationServiceProvider(serviceProvider);
        }

        public virtual void Dispose() {
            if (_dbConnection.State == ConnectionState.Open) _dbConnection.Close();
            _dbConnection.Dispose();
        }
    }
}
