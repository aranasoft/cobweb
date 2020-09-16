using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class TableBasedEntityMapping : IEntityTypeConfiguration<TableBasedEntity>
    {
        public void Configure(EntityTypeBuilder<TableBasedEntity> builder) {
            builder.Ignore(entity => entity.ViewEntity);
        }
    }
}
