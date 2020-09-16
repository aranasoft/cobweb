using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class TableBasedEntity {
        public int Id { get; set; }
        [MaxLength(256)]
        public string Field { get; set; }
        [Required]
        public IdentityRole<int> Role { get; set; }
        public ViewBasedEntity ViewEntity { get; set; }
    }
}
