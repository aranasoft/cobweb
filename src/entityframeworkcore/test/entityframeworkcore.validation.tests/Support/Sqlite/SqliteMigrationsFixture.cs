using System;
using System.Data;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite {
    public class SqliteMigrationsFixture<TMigration> : SqliteDatabaseFixture where TMigration : IMigration {
        ServiceProvider _serviceProvider;
        IServiceScope _scope;

        [OneTimeSetUp]
        public void ExecuteMigrations() {
            var services = new ServiceCollection()
                           .AddFluentMigratorCore()
                           .ConfigureRunner(run => run.AddSQLite()
                                                      .WithGlobalCommandTimeout(TimeSpan.FromMilliseconds(1500)))
                           .AddScoped<IDbConnection>(sp => GetContext().Database.GetDbConnection())
                           .AddScoped<SqliteTestingProcessor>()
                           .AddScoped<IMigrationProcessor>(sp => sp.GetRequiredService<SqliteTestingProcessor>())
                           .Configure<FluentMigratorLoggerOptions>(cfg => {
                               cfg.ShowElapsedTime = true;
                               cfg.ShowSql = true;
                           })
                           .Configure<SelectingProcessorAccessorOptions>(cfg => { cfg.ProcessorId = "SQLite-Test"; });

            _serviceProvider = services.BuildServiceProvider();
            _scope = _serviceProvider.CreateScope();

            var runner = _scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.Up(Activator.CreateInstance<TMigration>());
        }

        [OneTimeTearDown]
        public void DisposeMigrationScope() {
            _scope.Dispose();
            _serviceProvider.Dispose();
        }
    }
}
