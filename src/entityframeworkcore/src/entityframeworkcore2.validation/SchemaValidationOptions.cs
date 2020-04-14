namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    /// <summary>
    /// Configuration options for validating a model against a connected database
    /// </summary>
    public class SchemaValidationOptions {
        /// <summary>
        /// Validate indexes configured within the model against the connected database
        /// </summary>
        public bool ValidateIndexes { get; set; } = true;

        /// <summary>
        /// Validate foreign keys configured within the model against the connected database
        /// </summary>
        public bool ValidateForeignKeys { get; set; } = true;

        /// <summary>
        /// Validate nullability on table columns
        /// </summary>
        public bool ValidateNullabilityForTables { get; set; } = true;
    }
}
