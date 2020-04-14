using System;
using System.ComponentModel.DataAnnotations;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class ViewBasedEntity {
        public int Id { get; set; }
        [MaxLength(256)]
        public string Email { get; set; }
    }
}
