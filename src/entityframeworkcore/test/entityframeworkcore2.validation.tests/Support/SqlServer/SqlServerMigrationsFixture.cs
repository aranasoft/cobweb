using System;
using System.Data;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer {
    public class SqlServerMigrationsFixture<TMigration> : SqlServerLocalDbFixture where TMigration : IMigration {
        private readonly ServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;

        public SqlServerMigrationsFixture() {
            var services = new ServiceCollection()
                           .AddFluentMigratorCore()
                           .ConfigureRunner(run => run.AddSqlServer2016()
                                                      .WithGlobalCommandTimeout(TimeSpan.FromMilliseconds(10000)))
                           .AddScoped<IDbConnection>(sp => GetContext().Database.GetDbConnection())
                           .AddScoped<SqlServerTestingProcessor>()
                           .AddScoped<IMigrationProcessor>(sp => sp.GetRequiredService<SqlServerTestingProcessor>())
                           .Configure<FluentMigratorLoggerOptions>(cfg => {
                               cfg.ShowElapsedTime = true;
                               cfg.ShowSql = true;
                           })
                           .Configure<SelectingProcessorAccessorOptions>(cfg => {
                               cfg.ProcessorId = "SqlServer-Test";
                           });

            _serviceProvider = services.BuildServiceProvider();
            _scope = _serviceProvider.CreateScope();

            var runner = _scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.Up(Activator.CreateInstance<TMigration>());
        }

        public override void Dispose() {
            _scope.Dispose();
            _serviceProvider.Dispose();
            base.Dispose();
        }
    }
}
