using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    public class SchemaValidator {
        protected DbContext Context { get; }

        public SchemaValidator(DbContext context) {
            Context = context;
        }

        public void ValidateSchema(SchemaValidationOptions validationOptions = null) {
            validationOptions ??= new SchemaValidationOptions();

            var databaseModel = GetDatabaseModel();

            var entityModel = Context.Model;
            var validationErrors = new List<string>();
            var persistedTypes = entityModel.GetEntityTypes();

            foreach (var persistedType in persistedTypes) {
                if (persistedType.GetViewName() != null &&
                    databaseModel.GetView(persistedType) == null) {
                    validationErrors.Add($"Missing view: {persistedType.GetViewName()}");

                    if (persistedType.GetTableName() == null) {
                        continue;
                    }
                }

                if (persistedType.GetTableName() != null &&
                    databaseModel.GetTable(persistedType) == null) {
                    validationErrors.Add($"Missing table: {persistedType.GetTableName()}");

                    if (persistedType.GetViewName() == null) {
                        continue;
                    }
                }

                validationErrors.AddRange(ValidateColumns(databaseModel, persistedType, validationOptions));

                // For now, only validate Indexes and FKs on Tables
                if (persistedType.GetTableName() != null) {
                    if (validationOptions.ValidateIndexes) {
                        validationErrors.AddRange(ValidateIndexes(databaseModel, persistedType));
                    }

                    if (validationOptions.ValidateForeignKeys) {
                        validationErrors.AddRange(ValidateForeignKeys(databaseModel, persistedType));
                    }
                }
            }

            if (validationErrors.Count > 0) {
                throw new SchemaValidationException("Schema validation failed", validationErrors);
            }
        }

        private List<string> ValidateColumns(DatabaseModel databaseModel,
                                             IEntityType persistedType,
                                             SchemaValidationOptions validationOptions) {
            var valErrors = new List<string>();
            foreach (var persistedColumn in persistedType.GetProperties()
                                                         .Where(theProperty => theProperty.GetBeforeSaveBehavior() != PropertySaveBehavior.Ignore ||
                                                                               theProperty.GetAfterSaveBehavior() != PropertySaveBehavior.Ignore ||
                                                                               theProperty.FindAnnotation(RelationalAnnotationNames.IsStored)?.Value is true)
                                                         ) {
                if (persistedType.GetTableName() != null) {
                    var dbColumn = databaseModel.GetTableColumn(persistedColumn);
                    if (dbColumn == null) {
                        valErrors.Add(
                            $"Missing column: {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))} in {persistedType.GetTableName()}");
                        continue;
                    }

                    var columnTypesMatch =
                        dbColumn.StoreType.Replace(", ",",").Equals(persistedColumn.GetColumnType().Replace(", ",","), StringComparison.OrdinalIgnoreCase);
                    if (!columnTypesMatch) {
                        valErrors.Add(
                            $"Column type mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))}. Found: {dbColumn.StoreType.ToLowerInvariant()}, Expected {persistedColumn.GetColumnType().ToLowerInvariant()}");
                    }

                    if (validationOptions.ValidateNullabilityForTables && persistedColumn.IsNullable != dbColumn.IsNullable) {
                        valErrors.Add(
                            $"Column nullability mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))}. Found: {(dbColumn.IsNullable ? "Nullable" : "NotNullable")}, Expected {(persistedColumn.IsNullable ? "Nullable" : "NotNullable")}");
                    }

                    if (persistedColumn.GetDefaultValue() != null || persistedColumn.GetDefaultValueSql() != null || dbColumn.DefaultValueSql != null) {
                        if (persistedColumn.GetDefaultValue() != null && persistedColumn.GetDefaultValue()?.ToString() != dbColumn.DefaultValueSql) {
                            valErrors.Add(
                                $"Column default value mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))}. Found: {dbColumn.DefaultValueSql ?? "<none>"}, Expected: {persistedColumn.GetDefaultValue() ?? "<none>"}");
                        }

                        if (persistedColumn.GetDefaultValueSql() != null && persistedColumn.GetDefaultValueSql() != dbColumn.DefaultValueSql) {
                            valErrors.Add(
                                $"Column default value mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))}. Found: {dbColumn.DefaultValueSql ?? "<none>"}, Expected: {persistedColumn.GetDefaultValueSql() ?? "<none>"}");
                        }

                        if (persistedColumn.GetDefaultValue() == null && persistedColumn.GetDefaultValueSql() == null && dbColumn.DefaultValueSql != null) {
                            valErrors.Add(
                                $"Column default value mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))}. Found: {dbColumn.DefaultValueSql ?? "<none>"}, Expected: <none>");
                        }
                    }
                }

                if (persistedType.GetViewName() != null) {
                    var dbColumn = databaseModel.GetViewColumn(persistedColumn);
                    if (dbColumn == null) {
                        valErrors.Add(
                            $"Missing column: {persistedColumn.GetColumnName(StoreObjectIdentifier.View(persistedType.GetViewName(), null))} in {persistedType.GetViewName()}");
                        continue;
                    }

                    var columnTypesMatch =
                        dbColumn.StoreType.Replace(", ",",").Equals(persistedColumn.GetColumnType().Replace(", ",","), StringComparison.OrdinalIgnoreCase);
                    if (!columnTypesMatch) {
                        valErrors.Add(
                            $"Column type mismatch in {persistedType.GetViewName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.View(persistedType.GetViewName(), null))}. Found: {dbColumn.StoreType.ToLowerInvariant()}, Expected {persistedColumn.GetColumnType().ToLowerInvariant()}");
                    }

                    if (validationOptions.ValidateNullabilityForViews && persistedColumn.IsNullable != dbColumn.IsNullable) {
                        valErrors.Add(
                            $"Column nullability mismatch in {persistedType.GetViewName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.View(persistedType.GetViewName(), null))}. Found: {(dbColumn.IsNullable ? "Nullable" : "NotNullable")}, Expected {(persistedColumn.IsNullable ? "Nullable" : "NotNullable")}");
                    }
                }
            }

            return valErrors;
        }

        private IEnumerable<string> ValidateIndexes(DatabaseModel databaseModel, IEntityType persistedType) {
            var validationErrors = new List<string>();

            foreach (var index in persistedType.GetIndexes()) {
                var dbIndex = databaseModel.GetIndex(index);
                if (dbIndex == null) {
                    validationErrors.Add(
                        $"Missing index: {index.GetDatabaseName()} on {persistedType.GetTableName()}");
                    continue;
                }

                if (index.IsUnique != dbIndex.IsUnique) {
                    validationErrors.Add(
                        $"Index uniqueness mismatch: {index.GetDatabaseName()} on {persistedType.GetTableName()}. Found: {(dbIndex.IsUnique ? "Unique" : "Non-Unique")}, Expected: {(index.IsUnique ? "Unique" : "Non-Unique")}");
                }
            }

            return validationErrors;
        }

        private IEnumerable<string> ValidateForeignKeys(DatabaseModel databaseModel, IEntityType persistedType) {
            var validationErrors = new List<string>();

            foreach (var foreignKey in persistedType.GetForeignKeys()
                                                    .Where(key => key.PrincipalEntityType.FindAnnotation(
                                                                      RelationalAnnotationNames.ViewDefinitionSql) ==
                                                                  null)) {
                var databaseForeignKey = databaseModel.GetForeignKey(foreignKey);
                if (databaseForeignKey == null) {
                    validationErrors.Add(
                        $"Missing Foreign Key: {foreignKey.GetConstraintName()} on {persistedType.GetTableName()}");
                }
            }

            return validationErrors;
        }

        private DatabaseModel GetDatabaseModel() {
            var factory = Context.GetService<IDatabaseModelFactory>();
            var databaseModel = factory.Create(Context.Database.GetDbConnection(), new DatabaseModelFactoryOptions());
            return databaseModel;
        }
    }
}
