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
            var persistedTypes = entityModel.GetEntityTypes();

            foreach (var persistedType in persistedTypes) {
                if (databaseModel.GetTable(persistedType) == null) {
                    validationErrors.Add(persistedType.FindAnnotation(RelationalAnnotationNames.ViewDefinition) != null
                                             ? $"Missing view: {persistedType.GetTableName()}"
                                             : $"Missing table: {persistedType.GetTableName()}");

                    continue;
                }

                validationErrors.AddRange(ValidateColumns(databaseModel, persistedType, validationOptions));

                if (validationOptions.ValidateIndexes && persistedType.FindAnnotation(RelationalAnnotationNames.ViewDefinition) == null) {
                    validationErrors.AddRange(ValidateIndexes(databaseModel, persistedType));
                }

                if (validationOptions.ValidateForeignKeys && persistedType.FindAnnotation(RelationalAnnotationNames.ViewDefinition) == null) {
                    validationErrors.AddRange(ValidateForeignKeys(databaseModel, persistedType));
                }
            }

            if (validationErrors.Count > 0) {
                throw new SchemaValidationException("Schema validation failed", validationErrors);
            }
        }

        private List<string> ValidateColumns(DatabaseModel databaseModel, IEntityType persistedType, SchemaValidationOptions validationOptions) {
            var valErrors = new List<string>();
            foreach (var persistedColumn in persistedType.GetProperties()) {
                var dbColumn = databaseModel.GetColumn(persistedColumn);
                if (dbColumn == null) {
                    valErrors.Add($"Missing column: {persistedColumn.GetColumnName()} in {persistedType.GetTableName()}");
                    continue;
                }

                var columnTypesMatch =
                    dbColumn.StoreType.Equals(persistedColumn.GetColumnType(), StringComparison.OrdinalIgnoreCase);
                if (!columnTypesMatch) {
                    valErrors.Add(
                        $"Column type mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName()}. Found: {dbColumn.StoreType.ToLowerInvariant()}, Expected {persistedColumn.GetColumnType().ToLowerInvariant()}");
                }

                var isViewType = persistedType.FindAnnotation(RelationalAnnotationNames.ViewDefinition) != null;
                var shouldValidateColumnNullability = (validationOptions.ValidateNullabilityForTables && !isViewType) || (validationOptions.ValidateNullabilityForViews && isViewType);
                if (shouldValidateColumnNullability && persistedColumn.IsNullable != dbColumn.IsNullable) {
                    valErrors.Add(
                        $"Column nullability mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName()}. Found: {(dbColumn.IsNullable ? "Nullable" : "NotNullable")}, Expected {(persistedColumn.IsNullable ? "Nullable" : "NotNullable")}");
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
                        $"Missing index: {index.GetName()} on {persistedType.GetTableName()}");
                }
            }

            return validationErrors;
        }

        private IEnumerable<string> ValidateForeignKeys(DatabaseModel databaseModel, IEntityType persistedType) {
            var validationErrors = new List<string>();

            foreach (var foreignKey in persistedType.GetForeignKeys().Where(key => key.PrincipalEntityType.FindAnnotation(RelationalAnnotationNames.ViewDefinition) == null)) {
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
