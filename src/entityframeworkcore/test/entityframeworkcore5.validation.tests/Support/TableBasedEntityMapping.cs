using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class TableBasedEntityMapping : IEntityTypeConfiguration<TableBasedEntity> {
        public void Configure(EntityTypeBuilder<TableBasedEntity> builder) {
            builder.HasOne(table => table.Role).WithMany();
            builder.Property(table => table.ComputedNumber).HasComputedColumnSql("[Id] * 2");
            builder.Property(table => table.ComputedAndStoredNumber).HasComputedColumnSql("[Id] * 5", stored: true);
        }
    }
}
