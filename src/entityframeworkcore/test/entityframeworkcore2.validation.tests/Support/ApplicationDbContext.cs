using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support
{
    public class ApplicationDbContext : IdentityDbContext< IdentityUser<int>,  IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ViewBasedEntityMapping());
        }
    }
}
