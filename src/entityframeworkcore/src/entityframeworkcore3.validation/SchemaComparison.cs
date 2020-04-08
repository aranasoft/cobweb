using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    internal static class SchemaComparison {
        public static bool TableExists(this DatabaseModel model, IEntityType type) {
            return TableExists(model, type.GetSchema() ?? model.DefaultSchema, type.GetTableName());
        }

        public static bool TableExists(this DatabaseModel model, string schema, string tableName) {
            var checkedSchema = schema ?? model.DefaultSchema;
            return model.Tables.Any(table => table.Schema == checkedSchema && table.Name == tableName);
        }

        public static DatabaseTable GetTable(this DatabaseModel model, IEntityType type) {
            return GetTable(model, type.GetSchema() ?? model.DefaultSchema, type.GetTableName());
        }

        public static DatabaseTable GetTable(DatabaseModel model, string schema, string tableName) {
            var checkedSchema = schema ?? model.DefaultSchema;
            return model.Tables.FirstOrDefault(table => table.Schema == checkedSchema && table.Name == tableName);
        }

        public static bool ColumnExists(this DatabaseModel model, IProperty property) {
            return ColumnExists(model, property.DeclaringEntityType, property);
        }

        public static bool ColumnExists(this DatabaseModel model,
                                        IEntityType type,
                                        IProperty property) {
            return ColumnExists(model, type.GetSchema(), type.GetTableName(), property.GetColumnName());
        }

        public static bool ColumnExists(this DatabaseModel model, string schema, string tableName, string columnName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.Columns.Any(column => column.Name == columnName);
        }

        public static DatabaseColumn GetColumn(this DatabaseModel model, IProperty property) {
            return GetColumn(model, property.DeclaringEntityType, property);
        }

        public static DatabaseColumn GetColumn(this DatabaseModel model, IEntityType type, IProperty property) {
            return GetColumn(model, type.GetSchema(), type.GetTableName(), property.GetColumnName());
        }

        public static DatabaseColumn GetColumn(this DatabaseModel model,
                                               string schema,
                                               string tableName,
                                               string columnName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel?.Columns.FirstOrDefault(column => column.Name == columnName);
        }

        public static bool IndexExists(this DatabaseModel model, IIndex index) {
            var entityType = index.DeclaringEntityType;
            return IndexExists(model, entityType.GetSchema(), entityType.GetTableName(), index.GetName());
        }

        public static bool IndexExists(this DatabaseModel model, string schema, string tableName, string indexName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.Indexes.Any(column => column.Name == indexName);
        }

        public static DatabaseIndex GetIndex(this DatabaseModel model, IIndex index) {
            var entityType = index.DeclaringEntityType;
            return GetIndex(model, entityType.GetSchema(), entityType.GetTableName(), index.GetName());
        }

        public static DatabaseIndex GetIndex(this DatabaseModel model,
                                             string schema,
                                             string tableName,
                                             string indexName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel?.Indexes.FirstOrDefault(index => index.Name == indexName);
        }

        public static bool ForeignKeyExists(this DatabaseModel model,
                                            IForeignKey foreignKey) {
            var entityType = foreignKey.DeclaringEntityType;
            return ForeignKeyExists(model, entityType.GetSchema(), entityType.GetTableName(), foreignKey.GetConstraintName());
        }

        public static bool ForeignKeyExists(this DatabaseModel model,
                                            string schema,
                                            string tableName,
                                            string foreignKeyName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.ForeignKeys.Any(column => column.Name == foreignKeyName);
        }

        public static DatabaseForeignKey GetForeignKey(this DatabaseModel model, IForeignKey foreignKey) {
            var entityType = foreignKey.DeclaringEntityType;
            return GetForeignKey(model, entityType.GetSchema(), entityType.GetTableName(), foreignKey.GetConstraintName());
        }

        public static DatabaseForeignKey GetForeignKey(this DatabaseModel model,
                                                       string schema,
                                                       string tableName,
                                                       string foreignKeyName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel?.ForeignKeys.FirstOrDefault(index => index.Name == foreignKeyName);
        }
    }
}
