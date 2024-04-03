# Aranasoft.Cobweb.EntityFramework.Validation
![Azure DevOps](https://dev.azure.com/aranasoft/Cobweb/_apis/build/status/Aranasoft.Cobweb.EntityFrameworkCore?branchName=master)

Schema validation and testing components for Entity Framework Core.

Cobweb was created by [Arana Software](https://www.aranasoft.com), a software agency in Las Vegas, Nevada.

## Details

This library provides a set of tools for validating the schema of a DbContext in Entity Framework Core against the connected database. It is useful for ensuring that your EF Core model is in sync with your database schema.

## Features

 - Validates tables, views, columns, indexes, and foreign keys.
 - Allows you to specify which aspects of the schema to validate.
 - Provides detailed error messages when validation fails.

### Validations

**Tables**
 - Table presence
 - Column presence
 - Column type
 - Column nullability
 - Column default values _(Entity Framework Core 3.x and later)_

**Views** _(Entity Framework Core 3.x and later)_
 - View presence
 - Column presence
 - Column type
 - Column nullability

**Indexes**
 - Index presence
 - Index uniqueness _(Entity Framework Core 3.x and later)_

**Foreign Keys**
 - Foreign Key presence

## Installation

You can install the Aranasoft.Cobweb.EntityFrameworkCore.Validation NuGet package into your project.

```bash
dotnet add package Aranasoft.Cobweb.EntityFrameworkCore.Validation
```

Or via the NuGet Package Manager:

```bash
Install-Package Aranasoft.Cobweb.EntityFrameworkCore.Validation
```


## Usage

```csharp
using Aranasoft.Cobweb.EntityFrameworkCore.Validation

// ...

myApplicationContext.ValidateSchema([options]);
```

This will validate the schema of the DbContext against the connected database. If there are any discrepancies, a SchemaValidationException will be thrown.

`ValidateSchema` requires that the `DbContext` uses an Application Service Provider configured with platform-specific design time services.

Example:
```csharp
  var serviceCollection = new ServiceCollection().AddEntityFrameworkDesignTimeServices();
  new SqlServerDesignTimeServices().ConfigureDesignTimeServices(serviceCollection);
  var serviceProvider = serviceCollection.BuildServiceProvider();

  var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
  builder.UseSqlServer(myConnection);
  builder.UseApplicationServiceProvider(serviceProvider);

  var myApplicationContext = new ApplicationDbContext(builder.Options);
```

## Options

```csharp
using Aranasoft.Cobweb.EntityFrameworkCore.Validation

// ...

var options = new SchemaValidationOptions{
                      ValidateIndexes = true,
                      ValidateForeignKeys = true
                  };
```

### ValidateIndexes

Type: `boolean`<br>
Default: `true`

Set to `false` to skip validation of indexes.

### ValidateForeignKeys

Type: `boolean`<br>
Default: `true`

Set to `false` to skip validation of foreign keys. Useful for platforms that do no use foreign keys.

### ValidateNullabilityForTables

Type: `boolean`<br>
Default: `true`

Set to `false` to skip validation of nullability on table columns.

### ValidateNullabilityForViews

Type: `boolean`<br>
Default: `false`

Set to `false` to skip validation of nullability on view columns. By default, many database platforms enable nullability on view columns regardless of nullability on the underlying table column.

*This option is not applicable to Entity Framework Core 2.x or Aranasoft.Cobweb.EntityFrameworkCore.Validation 1.2x.x.*


## License

Cobweb is copyright of Arana Software, released under the [BSD License](http://opensource.org/licenses/BSD-3-Clause).
