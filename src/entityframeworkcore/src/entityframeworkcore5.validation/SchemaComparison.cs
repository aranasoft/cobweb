using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    internal static class SchemaComparison {
        public static bool TableExists(this DatabaseModel model, IEntityType type) {
            return TableExists(model, type.GetSchema() ?? model.DefaultSchema, type.GetTableName());
        }

        public static bool ViewExists(this DatabaseModel model, string schema, string viewName) {
            var checkedSchema = schema ?? model.DefaultSchema;
            return model.Tables.Any(table => table.Schema == checkedSchema && table.Name == viewName);
        }

        public static DatabaseTable GetView(this DatabaseModel model, IEntityType type) {
            return GetTable(model, type.GetViewSchema() ?? model.DefaultSchema, type.GetViewName());
        }

        public static DatabaseTable GetView(DatabaseModel model, string schema, string viewName) {
            var checkedSchema = schema ?? model.DefaultSchema;
            return model.Tables.FirstOrDefault(table => table.Schema == checkedSchema && table.Name == viewName);
        }

        public static bool ViewColumnExists(this DatabaseModel model, IProperty property) {
            return ViewColumnExists(model, property.DeclaringEntityType, property);
        }

        public static bool ViewColumnExists(this DatabaseModel model,
                                            IEntityType type,
                                            IProperty property) {
            return ViewColumnExists(model,
                                    type.GetViewSchema(),
                                    type.GetViewName(),
                                    property.GetColumnName(StoreObjectIdentifier.View(type.GetViewName(), type.GetSchema())));
        }

        public static bool ViewColumnExists(this DatabaseModel model,
                                            string schema,
                                            string viewName,
                                            string columnName) {
            var viewModel = GetView(model, schema, viewName);
            return viewModel != null && viewModel.Columns.Any(column => column.Name == columnName);
        }

        public static DatabaseColumn GetViewColumn(this DatabaseModel model, IProperty property) {
            return GetViewColumn(model, property.DeclaringEntityType, property);
        }

        public static DatabaseColumn GetViewColumn(this DatabaseModel model, IEntityType type, IProperty property) {
            return GetViewColumn(model,
                                 type.GetViewSchema(),
                                 type.GetViewName(),
                                 property.GetColumnName(StoreObjectIdentifier.View(type.GetViewName(), type.GetSchema())));
        }

        public static DatabaseColumn GetViewColumn(this DatabaseModel model,
                                                   string schema,
                                                   string viewName,
                                                   string columnName) {
            var viewModel = GetView(model, schema, viewName);
            return viewModel?.Columns.FirstOrDefault(column => column.Name == columnName);
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

        public static bool TableColumnExists(this DatabaseModel model, IProperty property) {
            return TableColumnExists(model, property.DeclaringEntityType, property);
        }

        public static bool TableColumnExists(this DatabaseModel model,
                                             IEntityType type,
                                             IProperty property) {
            return TableColumnExists(model,
                                     type.GetSchema(),
                                     type.GetTableName(),
                                     property.GetColumnName(
                                         StoreObjectIdentifier.Table(type.GetTableName(), type.GetSchema())));
        }

        public static bool TableColumnExists(this DatabaseModel model,
                                             string schema,
                                             string tableName,
                                             string columnName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.Columns.Any(column => column.Name == columnName);
        }

        public static DatabaseColumn GetTableColumn(this DatabaseModel model, IProperty property) {
            return GetTableColumn(model, property.DeclaringEntityType, property);
        }

        public static DatabaseColumn GetTableColumn(this DatabaseModel model, IEntityType type, IProperty property) {
            return GetTableColumn(model,
                                  type.GetSchema(),
                                  type.GetTableName(),
                                  property.GetColumnName(
                                      StoreObjectIdentifier.Table(type.GetTableName(), type.GetSchema())));
        }

        public static DatabaseColumn GetTableColumn(this DatabaseModel model,
                                                    string schema,
                                                    string tableName,
                                                    string columnName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel?.Columns.FirstOrDefault(column => column.Name == columnName);
        }

        public static bool IndexExists(this DatabaseModel model, IIndex index) {
            var entityType = index.DeclaringEntityType;
            return IndexExists(model, entityType.GetSchema(), entityType.GetTableName(), index.GetDatabaseName());
        }

        public static bool IndexExists(this DatabaseModel model, string schema, string tableName, string indexName) {
            var tableModel = GetTable(model, schema, tableName);
            return tableModel != null && tableModel.Indexes.Any(column => column.Name == indexName);
        }

        public static DatabaseIndex GetIndex(this DatabaseModel model, IIndex index) {
            var entityType = index.DeclaringEntityType;
            return GetIndex(model, entityType.GetSchema(), entityType.GetTableName(), index.GetDatabaseName());
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
            return ForeignKeyExists(model,
                                    entityType.GetSchema(),
                                    entityType.GetTableName(),
                                    foreignKey.GetConstraintName());
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
            return GetForeignKey(model,
                                 entityType.GetSchema(),
                                 entityType.GetTableName(),
                                 foreignKey.GetConstraintName());
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
