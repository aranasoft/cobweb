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
            validationOptions ??= new SchemaValidationOptions();

            var databaseModel = GetDatabaseModel();

            var entityModel = Context.Model;
            var validationErrors = new List<string>();
            var persistedTypes = entityModel.GetEntityTypes();

            foreach (var persistedType in persistedTypes) {
                if (databaseModel.GetView(persistedType) == null &&
                    persistedType.FindAnnotation(RelationalAnnotationNames.ViewName)?.Value != null) {
                    validationErrors.Add($"Missing view: {persistedType.GetViewName()}");

                    if (persistedType.FindAnnotation(RelationalAnnotationNames.TableName)?.Value == null) {
                        continue;
                    }
                }

                if (databaseModel.GetTable(persistedType) == null &&
                    persistedType.FindAnnotation(RelationalAnnotationNames.TableName)?.Value != null) {
                    validationErrors.Add($"Missing table: {persistedType.GetTableName()}");

                    if (persistedType.FindAnnotation(RelationalAnnotationNames.ViewName)?.Value == null) {
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
            var valErrors = new List<string>();
            foreach (var persistedColumn in persistedType.GetProperties()
                                                         .Where(theProperty => theProperty.GetBeforeSaveBehavior() != PropertySaveBehavior.Ignore ||
                                                                               theProperty.GetAfterSaveBehavior() != PropertySaveBehavior.Ignore ||
                                                                               theProperty.FindAnnotation(RelationalAnnotationNames.IsStored)?.Value is true)
                                                         ) {
                if (persistedType.FindAnnotation(RelationalAnnotationNames.TableName)?.Value != null) {
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

                    if (persistedColumn.FindAnnotation(RelationalAnnotationNames.DefaultValue) != null || persistedColumn.GetDefaultValueSql() != null || dbColumn.DefaultValueSql != null) {
                        if (persistedColumn.FindAnnotation(RelationalAnnotationNames.DefaultValue) != null && persistedColumn.FindAnnotation(RelationalAnnotationNames.DefaultValue).Value.ToString() != dbColumn.DefaultValueSql) {
                            valErrors.Add(
                                $"Column default value mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))}. Found: {dbColumn.DefaultValueSql ?? "<none>"}, Expected: {persistedColumn.FindAnnotation(RelationalAnnotationNames.DefaultValue).Value.ToString() ?? "<none>"}");
                        }

                        if (persistedColumn.FindAnnotation(RelationalAnnotationNames.DefaultValueSql) != null && persistedColumn.FindAnnotation(RelationalAnnotationNames.DefaultValueSql).Value.ToString() != dbColumn.DefaultValueSql) {
                            valErrors.Add(
                                $"Column default value mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))}. Found: {dbColumn.DefaultValueSql ?? "<none>"}, Expected: {persistedColumn.FindAnnotation(RelationalAnnotationNames.DefaultValueSql).Value.ToString() ?? "<none>"}");
                        }

                        if (persistedColumn.FindAnnotation(RelationalAnnotationNames.DefaultValue) == null && persistedColumn.GetDefaultValueSql() == null && dbColumn.DefaultValueSql != null) {
                            valErrors.Add(
                                $"Column default value mismatch in {persistedType.GetTableName()} for column {persistedColumn.GetColumnName(StoreObjectIdentifier.Table(persistedType.GetTableName(), null))}. Found: {dbColumn.DefaultValueSql ?? "<none>"}, Expected: <none>");
                        }
                    }
                }

                if (persistedType.FindAnnotation(RelationalAnnotationNames.ViewName)?.Value != null) {
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

        /// <summary>
        /// Validates the indexes of a persisted type.
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="persistedType">The persisted type to validate.</param>
        /// <returns>A list of validation errors.</returns>
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

        /// <summary>
        /// Validates the foreign keys of a persisted type.
        /// </summary>
        /// <param name="databaseModel">The database model.</param>
        /// <param name="persistedType">The persisted type to validate.</param>
        /// <returns>A list of validation errors.</returns>
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
            var databaseModel = factory.Create(Context.Database.GetDbConnection(), new DatabaseModelFactoryOptions());
            return databaseModel;
        }
    }
}
