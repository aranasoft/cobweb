using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    internal static class SchemaComparison {
        public static bool TableExists(this DatabaseModel model, IEntityType type) {
            return TableExists(model, type.Relational());
        }

        public static bool TableExists(this DatabaseModel model, IRelationalEntityTypeAnnotations table) {
            return TableExists(model, table.Schema ?? model.DefaultSchema, table.TableName);
        }

        public static bool TableExists(this DatabaseModel model, string schema, string tableName) {
            var checkedSchema = schema ?? model.DefaultSchema;
            return model.Tables.Any(table => table.Schema == checkedSchema && table.Name == tableName);
        }

        public static DatabaseTable GetTable(this DatabaseModel model, IEntityType type) {
            return GetTable(model, type.Relational());
        }

        public static DatabaseTable GetTable(this DatabaseModel model, IRelationalEntityTypeAnnotations table) {
            return GetTable(model, table.Schema ?? model.DefaultSchema, table.TableName);
        }

        public static DatabaseTable GetTable(DatabaseModel model, string schema, string tableName) {
            var checkedSchema = schema ?? model.DefaultSchema;
            return model.Tables.FirstOrDefault(table => table.Schema == checkedSchema && table.Name == tableName);
        }

        public static bool ColumnExists(this DatabaseModel model, IProperty property) {
            return ColumnExists(model, property.DeclaringEntityType.Relational(), property.Relational());
        }

        public static bool ColumnExists(this DatabaseModel model,
                                        IRelationalEntityTypeAnnotations table,
                                        IRelationalPropertyAnnotations column) {
            return ColumnExists(model, table.Schema, table.TableName, column.ColumnName);
        }

        public static bool ColumnExists(this DatabaseModel model, string schema, string tableName, string columnName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.Columns.Any(column => column.Name == columnName);
        }

        public static DatabaseColumn GetColumn(this DatabaseModel model, IProperty property) {
            return GetColumn(model, property.DeclaringEntityType.Relational(), property.Relational());
        }

        public static DatabaseColumn GetColumn(this DatabaseModel model,
                                               IRelationalEntityTypeAnnotations table,
                                               IRelationalPropertyAnnotations column) {
            return GetColumn(model, table.Schema, table.TableName, column.ColumnName);
        }

        public static DatabaseColumn GetColumn(this DatabaseModel model,
                                               string schema,
                                               string tableName,
                                               string columnName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel?.Columns.FirstOrDefault(column => column.Name == columnName);
        }

        public static bool IndexExists(this DatabaseModel model, IIndex index) {
            var table = index.DeclaringEntityType.Relational();
            return IndexExists(model, table.Schema, table.TableName, index.Relational().Name);
        }

        public static bool IndexExists(this DatabaseModel model, string schema, string tableName, string indexName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.Indexes.Any(column => column.Name == indexName);
        }

        public static DatabaseIndex GetIndex(this DatabaseModel model, IIndex index) {
            var table = index.DeclaringEntityType.Relational();
            return GetIndex(model, table.Schema, table.TableName, index.Relational().Name);
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
            var table = foreignKey.DeclaringEntityType.Relational();
            return ForeignKeyExists(model, table.Schema, table.TableName, foreignKey.Relational().Name);
        }

        public static bool ForeignKeyExists(this DatabaseModel model,
                                            string schema,
                                            string tableName,
                                            string foreignKeyName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.ForeignKeys.Any(column => column.Name == foreignKeyName);
        }

        public static DatabaseForeignKey GetForeignKey(this DatabaseModel model, IForeignKey foreignKey) {
            var table = foreignKey.DeclaringEntityType.Relational();
            return GetForeignKey(model, table.Schema, table.TableName, foreignKey.Relational().Name);
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
