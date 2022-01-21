using Microsoft.EntityFrameworkCore;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    public static class SchemaValidatorExtensions {
        public static void ValidateSchema(this DbContext context, SchemaValidationOptions validationOptions = null) {
            var validator = new SchemaValidator(context);
            validator.ValidateSchema(validationOptions ?? new SchemaValidationOptions());
        }
    }
}
