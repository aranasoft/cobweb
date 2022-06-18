using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class TableBasedEntity {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public string IgnoredColumn { get; set; }

        [MaxLength(256)]
        public string Field { get; set; }

        [Column("NumberValue")]
        public decimal Number { get; set; }

        [Required]
        public IdentityRole<int> Role { get; set; }

        public int ComputedNumber { get;set; }
    }
}
