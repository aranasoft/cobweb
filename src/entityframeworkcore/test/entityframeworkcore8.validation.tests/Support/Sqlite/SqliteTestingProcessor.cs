using System;
using System.Data;
using FluentMigrator.Runner.Generators.SQLite;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SQLite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.Sqlite;
public class SqliteTestingProcessor : SQLiteProcessor {
    public SqliteTestingProcessor(IDbConnection connection,
                                  SQLiteDbFactory factory,
                                  SQLiteGenerator generator,
                                  ILogger<SQLiteProcessor> logger,
                                  IOptionsSnapshot<ProcessorOptions> options,
                                  IConnectionStringAccessor connectionStringAccessor,
                                  IServiceProvider serviceProvider) : base(
        factory,
        generator,
        logger,
        options,
        connectionStringAccessor,
        serviceProvider,
        new SQLiteQuoter()) {
        Connection = connection;
    }

    public override string DatabaseType => "SQLite-Test";

    protected override void EnsureConnectionIsClosed() {}
}
