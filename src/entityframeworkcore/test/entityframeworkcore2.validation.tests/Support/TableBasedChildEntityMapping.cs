using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class TableBasedChildEntityMapping : IEntityTypeConfiguration<TableBasedChildEntity>
    {
        public void Configure(EntityTypeBuilder<TableBasedChildEntity> builder) {
            builder.HasOne(child => child.ViewEntity).WithMany();
        }
    }
}
