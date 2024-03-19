using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    /// <summary>
    /// Provides methods for comparing the schema of a DbContext with the database model.
    /// </summary>
    internal static class SchemaComparison {
        /// <summary>
        /// Checks if a table exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="type">The entity type.</param>
        /// <returns>True if the table exists, false otherwise.</returns>
        public static bool TableExists(this DatabaseModel model, IEntityType type) {
            return TableExists(model, type.GetSchema() ?? model.DefaultSchema, type.GetTableName());
        }

        /// <summary>
        /// Checks if a table exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="schema">The schema name.</param>
        /// <param name="tableName">The table name.</param>
        /// <returns>True if the table exists, false otherwise.</returns>
        public static bool TableExists(this DatabaseModel model, string schema, string tableName) {
            var checkedSchema = schema ?? model.DefaultSchema;
            return model.Tables.Any(table => table.Schema == checkedSchema && table.Name == tableName);
        }

        /// <summary>
        /// Gets a table from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="type">The entity type.</param>
        /// <returns>The database table.</returns>
        public static DatabaseTable GetTable(this DatabaseModel model, IEntityType type) {
            return GetTable(model, type.GetSchema() ?? model.DefaultSchema, type.GetTableName());
        }

        /// <summary>
        /// Gets a table from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="schema">The schema name.</param>
        /// <param name="tableName">The table name.</param>
        /// <returns>The database table.</returns>
        public static DatabaseTable GetTable(DatabaseModel model, string schema, string tableName) {
            var checkedSchema = schema ?? model.DefaultSchema;
            return model.Tables.FirstOrDefault(table => table.Schema == checkedSchema && table.Name == tableName);
        }

        /// <summary>
        /// Checks if a column exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="property">The property.</param>
        /// <returns>True if the column exists, false otherwise.</returns>
        public static bool ColumnExists(this DatabaseModel model, IProperty property) {
            return ColumnExists(model, property.DeclaringEntityType, property);
        }

        /// <summary>
        /// Checks if a column exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="type">The entity type.</param>
        /// <param name="property">The property.</param>
        /// <returns>True if the column exists, false otherwise.</returns>
        public static bool ColumnExists(this DatabaseModel model,
                                        IEntityType type,
                                        IProperty property) {
            return ColumnExists(model, type.GetSchema(), type.GetTableName(), property.GetColumnName());
        }

        /// <summary>
        /// Checks if a column exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="schema">The schema name.</param>
        /// <param name="tableName">The table name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>True if the column exists, false otherwise.</returns>
        public static bool ColumnExists(this DatabaseModel model, string schema, string tableName, string columnName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.Columns.Any(column => column.Name == columnName);
        }

        /// <summary>
        /// Gets a column from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="property">The property.</param>
        /// <returns>The database column.</returns>
        public static DatabaseColumn GetColumn(this DatabaseModel model, IProperty property) {
            return GetColumn(model, property.DeclaringEntityType, property);
        }

        /// <summary>
        /// Gets a column from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="type">The entity type.</param>
        /// <param name="property">The property.</param>
        /// <returns>The database column.</returns>
        public static DatabaseColumn GetColumn(this DatabaseModel model, IEntityType type, IProperty property) {
            return GetColumn(model, type.GetSchema(), type.GetTableName(), property.GetColumnName());
        }

        /// <summary>
        /// Gets a column from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="schema">The schema name.</param>
        /// <param name="tableName">The table name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The database column.</returns>
        public static DatabaseColumn GetColumn(this DatabaseModel model,
                                               string schema,
                                               string tableName,
                                               string columnName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel?.Columns.FirstOrDefault(column => column.Name == columnName);
        }

        /// <summary>
        /// Checks if an index exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="index">The index.</param>
        /// <returns>True if the index exists, false otherwise.</returns>
        public static bool IndexExists(this DatabaseModel model, IIndex index) {
            var entityType = index.DeclaringEntityType;
            return IndexExists(model, entityType.GetSchema(), entityType.GetTableName(), index.GetName());
        }

        /// <summary>
        /// Checks if an index exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="schema">The schema name.</param>
        /// <param name="tableName">The table name.</param>
        /// <param name="indexName">The index name.</param>
        /// <returns>True if the index exists, false otherwise.</returns>
        public static bool IndexExists(this DatabaseModel model, string schema, string tableName, string indexName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.Indexes.Any(column => column.Name == indexName);
        }

        /// <summary>
        /// Gets an index from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="index">The index.</param>
        /// <returns>The database index.</returns>
        public static DatabaseIndex GetIndex(this DatabaseModel model, IIndex index) {
            var entityType = index.DeclaringEntityType;
            return GetIndex(model, entityType.GetSchema(), entityType.GetTableName(), index.GetName());
        }

        /// <summary>
        /// Gets an index from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="schema">The schema name.</param>
        /// <param name="tableName">The table name.</param>
        /// <param name="indexName">The index name.</param>
        /// <returns>The database index.</returns>
        public static DatabaseIndex GetIndex(this DatabaseModel model,
                                             string schema,
                                             string tableName,
                                             string indexName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel?.Indexes.FirstOrDefault(index => index.Name == indexName);
        }

        /// <summary>
        /// Checks if a foreign key exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="foreignKey">The foreign key.</param>
        /// <returns>True if the foreign key exists, false otherwise.</returns>
        public static bool ForeignKeyExists(this DatabaseModel model,
                                            IForeignKey foreignKey) {
            var entityType = foreignKey.DeclaringEntityType;
            return ForeignKeyExists(model,
                                    entityType.GetSchema(),
                                    entityType.GetTableName(),
                                    foreignKey.GetConstraintName());
        }

        /// <summary>
        /// Checks if a foreign key exists in the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="schema">The schema name.</param>
        /// <param name="tableName">The table name.</param>
        /// <param name="foreignKeyName">The foreign key name.</param>
        /// <returns>True if the foreign key exists, false otherwise.</returns>
        public static bool ForeignKeyExists(this DatabaseModel model,
                                            string schema,
                                            string tableName,
                                            string foreignKeyName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.ForeignKeys.Any(column => column.Name == foreignKeyName);
        }

        /// <summary>
        /// Gets a foreign key from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="foreignKey">The foreign key.</param>
        /// <returns>The database foreign key.</returns>
        public static DatabaseForeignKey GetForeignKey(this DatabaseModel model, IForeignKey foreignKey) {
            var entityType = foreignKey.DeclaringEntityType;
            return GetForeignKey(model,
                                 entityType.GetSchema(),
                                 entityType.GetTableName(),
                                 foreignKey.GetConstraintName());
        }

        /// <summary>
        /// Gets a foreign key from the database model.
        /// </summary>
        /// <param name="model">The database model.</param>
        /// <param name="schema">The schema name.</param>
        /// <param name="tableName">The table name.</param>
        /// <param name="foreignKeyName">The foreign key name.</param>
        /// <returns>The database foreign key.</returns>
        public static DatabaseForeignKey GetForeignKey(this DatabaseModel model,
                                                       string schema,
                                                       string tableName,
                                                       string foreignKeyName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel?.ForeignKeys.FirstOrDefault(index => index.Name == foreignKeyName);
        }
    }
}
