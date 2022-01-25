using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class ViewBasedEntity {
        public int Id { get; set; }

        [MaxLength(256)]
        public string? Field { get; set; }

        public IdentityRole<int> Role { get; set; }
        public TableBasedEntity TableEntity { get; set; }
    }
}
