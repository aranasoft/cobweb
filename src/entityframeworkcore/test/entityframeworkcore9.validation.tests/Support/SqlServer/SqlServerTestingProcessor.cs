using System;
using System.Data;
using FluentMigrator.Runner.Generators.SqlServer;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.SqlServer;
public class SqlServerTestingProcessor : SqlServer2016Processor {
    public SqlServerTestingProcessor(IDbConnection connection,
                                     SqlServer2016Generator generator,
                                     SqlServer2008Quoter quoter,
                                     ILogger<SqlServer2016Processor> logger,
                                     IOptionsSnapshot<ProcessorOptions> options,
                                     IConnectionStringAccessor connectionStringAccessor,
                                     IServiceProvider serviceProvider) : base(
        logger,
        quoter,
        generator,
        options,
        connectionStringAccessor,
        serviceProvider) {
        Connection = connection;
    }

    public override string DatabaseType => "SqlServer-Test";

    protected override void EnsureConnectionIsClosed() {}
}
