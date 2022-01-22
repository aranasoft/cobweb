using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    public class SchemaValidator {
        protected DbContext Context { get; }

        public SchemaValidator(DbContext context) {
            Context = context;
        }

        public void ValidateSchema(SchemaValidationOptions validationOptions = null) {
            if (validationOptions == null) {
                validationOptions = new SchemaValidationOptions();
            }

            var databaseModel = GetDatabaseModel();

            var entityModel = Context.Model;
            var validationErrors = new List<string>();
            var persistedTypes = entityModel.GetEntityTypes().Where(entityType => !entityType.IsQueryType);

            foreach (var persistedType in persistedTypes) {
                var entityTable = persistedType.Relational();
                var dbTable = databaseModel.GetTable(persistedType);

                if (dbTable == null) {
                    validationErrors.Add($"Missing table: {entityTable.TableName}");
                    continue;
                }

                validationErrors.AddRange(ValidateColumns(databaseModel, persistedType, validationOptions));

                if (validationOptions.ValidateIndexes) {
                    validationErrors.AddRange(ValidateIndexes(databaseModel, persistedType));
                }

                if (validationOptions.ValidateForeignKeys) {
                    validationErrors.AddRange(ValidateForeignKeys(databaseModel, persistedType));
                }
            }

            if (validationErrors.Count > 0) {
                throw new SchemaValidationException("Schema validation failed", validationErrors);
            }
        }

        private List<string> ValidateColumns(DatabaseModel databaseModel,
                                             IEntityType persistedType,
                                             SchemaValidationOptions validationOptions) {
            var entityTable = persistedType.Relational();
            var valErrors = new List<string>();
            foreach (var entityProperty in persistedType.GetProperties()) {
                var entityColumn = entityProperty.Relational();

                var dbColumn = databaseModel.GetColumn(entityProperty);
                if (dbColumn == null) {
                    valErrors.Add($"Missing column: {entityColumn.ColumnName} in {entityTable.TableName}");
                    continue;
                }

                var columnTypesMatch =
                    dbColumn.StoreType.Replace(", ",",").Equals(entityColumn.ColumnType.Replace(", ",","), StringComparison.OrdinalIgnoreCase);
                if (!columnTypesMatch) {
                    valErrors.Add(
                        $"Column type mismatch in {entityTable.TableName} for column {entityColumn.ColumnName}. Found: {dbColumn.StoreType.ToLowerInvariant()}, Expected {entityColumn.ColumnType.ToLowerInvariant()}");
                }

                var shouldValidateColumnNullability = validationOptions.ValidateNullabilityForTables;
                if (shouldValidateColumnNullability && entityProperty.IsNullable != dbColumn.IsNullable) {
                    valErrors.Add(
                        $"Column nullability mismatch in {entityTable.TableName} for column {entityColumn.ColumnName}. Found: {(dbColumn.IsNullable ? "Nullable" : "NotNullable")}, Expected {(entityProperty.IsNullable ? "Nullable" : "NotNullable")}");
                }
            }

            return valErrors;
        }

        private IEnumerable<string> ValidateIndexes(DatabaseModel databaseModel, IEntityType persistedType) {
            var entityTable = persistedType.Relational();
            var validationErrors = new List<string>();

            foreach (var index in persistedType.GetIndexes()) {
                var dbIndex = databaseModel.GetIndex(index);
                if (dbIndex == null) {
                    validationErrors.Add(
                        $"Missing index: {index.Relational().Name} on {entityTable.TableName}");
                    continue;
                }
            }

            return validationErrors;
        }

        private IEnumerable<string> ValidateForeignKeys(DatabaseModel databaseModel, IEntityType persistedType) {
            var entityTable = persistedType.Relational();
            var validationErrors = new List<string>();

            foreach (var foreignKey in persistedType.GetForeignKeys()
                                                    .Where(key => !key.PrincipalEntityType.IsQueryType)) {
                var databaseForeignKey = databaseModel.GetForeignKey(foreignKey);
                if (databaseForeignKey == null) {
                    validationErrors.Add(
                        $"Missing Foreign Key: {foreignKey.Relational().Name} on {entityTable.TableName}");
                    continue;
                }
            }

            return validationErrors;
        }

        private DatabaseModel GetDatabaseModel() {
            var factory = Context.GetService<IDatabaseModelFactory>();
            var databaseModel = factory.Create(Context.Database.GetDbConnection(),
                                               Enumerable.Empty<string>(),
                                               Enumerable.Empty<string>());
            return databaseModel;
        }
    }
}
