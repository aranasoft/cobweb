using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    /// <summary>
    /// Class responsible for validating the schema of a <see cref="DbContext"/>.
    /// </summary>
    public class SchemaValidator {
        /// <summary>
        /// The <see cref="DbContext"/> to validate.
        /// </summary>
        protected DbContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaValidator"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> to validate.</param>
        public SchemaValidator(DbContext context) {
            Context = context;
        }

        /// <summary>
        /// Validates the schema of the <see cref="DbContext"/>.
        /// </summary>
        /// <param name="validationOptions">The options for schema validation.</param>
        /// <exception cref="SchemaValidationException">Thrown when there are validation errors in the schema.</exception>
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

        /// <summary>
        /// Validates the columns of a persisted type.
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="persistedType">The persisted type to validate.</param>
        /// <param name="validationOptions">The options for schema validation.</param>
        /// <returns>A list of validation errors.</returns>
        private List<string> ValidateColumns(DatabaseModel databaseModel,
                                             IEntityType persistedType,
                                             SchemaValidationOptions validationOptions) {
            var entityTable = persistedType.Relational();
            var valErrors = new List<string>();
            foreach (var entityProperty in persistedType.GetProperties()
                                                        .Where(theProperty => theProperty.BeforeSaveBehavior != PropertySaveBehavior.Ignore &&
                                                                              theProperty.AfterSaveBehavior != PropertySaveBehavior.Ignore)) {
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

        /// <summary>
        /// Validates the indexes of a persisted type.
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="persistedType">The persisted type to validate.</param>
        /// <returns>A list of validation errors.</returns>
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

        /// <summary>
        /// Validates the foreign keys of a persisted type.
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="persistedType">The persisted type to validate.</param>
        /// <returns>A list of validation errors.</returns>
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

        /// <summary>
        /// Gets the database model.
        /// </summary>
        /// <returns>The database model.</returns>
        private DatabaseModel GetDatabaseModel() {
            var factory = Context.GetService<IDatabaseModelFactory>();
            var databaseModel = factory.Create(Context.Database.GetDbConnection(),
                                               Enumerable.Empty<string>(),
                                               Enumerable.Empty<string>());
            return databaseModel;
        }
    }
}
