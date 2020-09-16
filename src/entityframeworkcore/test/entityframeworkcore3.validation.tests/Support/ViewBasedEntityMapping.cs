using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class ViewBasedEntityMapping : IEntityTypeConfiguration<ViewBasedEntity>
    {
        public void Configure(EntityTypeBuilder<ViewBasedEntity> builder) {
            builder.ToView("ViewBasedEntities");
            builder.HasOne(viewBasedEntity => viewBasedEntity.TableEntity).WithOne(tableBasedEntity => tableBasedEntity.ViewEntity).HasForeignKey<ViewBasedEntity>(entity => entity.Id);
        }
    }
}
