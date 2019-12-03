# Cobweb.Data.FluentMigrator ![Visual Studio Team Services](https://aranasoft.visualstudio.com/_apis/public/build/definitions/e012ec23-f4c2-4d38-a0e0-133f48eb2542/19/badge?branchName=master&api-version=5.0-preview.2)

FluentMigrator utilities and classes for Cobweb, a utility library for .NET.

Cobweb was created by [Arana Software](https://www.aranasoft.com), a software agency in Las Vegas, Nevada.

## Alternate Syntax for FluentMigrator

`Cobweb.Data.FluentMigrator` provides an alternate syntax style for [FluentMigrator](https://fluentmigrator.github.io/), a database migration framework for .NET, using lambda expression syntax instead of method chains.

### FluentMigrator's Method Chain Syntax

FluentMigrator employes a series of chained expression to build database schema.

```
Create.Table("TestTable")
      .WithColumn("FirstColumn").AsString().Nullable().WithDefaultValue("test")
      .WithColumn("SecondColumn").AsString().NotNullable().WithDefaultValue("sample");
```

Though this style is effective, some may find the syntax confusing, as there is no clear delineation between setting column properties and transitioning additional columns. Because this grouping can only be resolved through the context of method names, it is also impossible for code-formatting tools to organize and group applicable code.

### Cobweb's Lambda Expression Syntax

Cobweb uses lambda expressions to define column properties.

```
Create.Table("TestTable")
      .WithColumn("FirstColumn", col => col.AsString().Nullable().WithDefaultValue("test"))
      .WithColumn("SecondColumn, col => col.AsString().NotNullable().WithDefaultValue("sample"));
```

Under the covers, Cobweb passes these lambda expressions to FluentMigrator's method chains. The lambda syntax will naturally group column properties and delineate from other columns. This grouping also enables code formating support from editor tooling.

## Installation

From Package Manager Console:

```bash
PM> install-package cobweb.data.fluentmigrator
```

From .NET CLI:

```bash
> dotnet add package Cobweb.Data.FluentMigrator
```

## Usage

### Create Table: `WithColumn`

Syntax: `WithColumn(string columnName, Func columnOptions)`

```
Create.Table("TestTable")
      .WithColumn("FirstColumn", col => col.AsString().Nullable())
      .WithColumn("SecondColumn, col => col.AsString().NotNullable().WithDefaultValue("sample"));
```

### Alter Table: `AddColumn`

Syntax: `AddColumn(string columnName, Func columnOptions)`

```
Alter.Table("TestTable")
     .AddColumn("FirstColumn", col => col.AsInt32().Nullable())
     .AddColumn("SecondColumn", col => col.AsInt32().NotNullable().WithDefaultValue(5)));
```

### Alter Table: `AlterColumn`

Syntax: `AlterColumn(string columnName, Func columnOptions)`

```
Alter.Table("TestTable")
     .AlterColumn("FirstExistingColumn", col => col.AsInt32())
     .AlterColumn("SecondExistingColumn", col => col.AsInt32().NotNullable().WithDefaultValue(5)));
```

### Create Index: `OnColumn`

Syntax: `OnColumn(string columnName, Func columnOptions)`

```
Create.Index("TestIndex")
     .OnColumn("FirstColumn", col => col.Ascending())
     .OnColumn("SecondColumn", col => col.Descending());
```

## License

Cobweb is copyright of Arana Software, released under the [BSD License](http://opensource.org/licenses/BSD-3-Clause).
