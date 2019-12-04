using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation {
    public class SchemaValidationException : Exception {
        public ReadOnlyCollection<string> ValidationErrors { get; }

        public SchemaValidationException(string message, IList<string> validationErrors) : base(message) {
            ValidationErrors = new ReadOnlyCollection<string>(validationErrors);
        }
    }
}
