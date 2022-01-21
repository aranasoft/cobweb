using System;
using System.ComponentModel.DataAnnotations;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class TableBasedChildEntity {
        [Key]
        public int Id { get; set; }

        [MaxLength(10000)]
        public string Name { get; set; }

        [Required]
        public ViewBasedEntity ViewEntity { get; set; }
    }
}
