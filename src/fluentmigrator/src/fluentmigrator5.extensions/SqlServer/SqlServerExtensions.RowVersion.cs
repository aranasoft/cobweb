using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create.Table;

namespace Aranasoft.Cobweb.FluentMigrator.Extensions.SqlServer {
    /// <summary>
    /// Provides extension methods for FluentMigrator to handle rowversion columns.
    /// </summary>
    public static partial class SqlServerExtensions {
        /// <summary>
        /// Defines a column as a rowversion in a CREATE TABLE statement.
        /// </summary>
        /// <param name="col">The column being defined. This represents a unique identifier.</param>
        /// <returns>An instance of <see cref="ICreateTableColumnOptionOrWithColumnSyntax"/> that can be used to further define the column or add additional columns.</returns>
        public static ICreateTableColumnOptionOrWithColumnSyntax AsRowVersion(this ICreateTableColumnAsTypeSyntax col) {
            return col.AsCustom("rowversion");
        }

        /// <summary>
        /// Defines a column as a rowversion in an ALTER TABLE statement.
        /// </summary>
        /// <param name="col">The column being altered. This represents a unique identifier.</param>
        /// <returns>An instance of <see cref="IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax"/> that can be used to further define the column or add additional columns.</returns>
        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AsRowVersion(this IAlterTableColumnAsTypeSyntax col) {
            return col.AsCustom("rowversion");
        }
    }
}
