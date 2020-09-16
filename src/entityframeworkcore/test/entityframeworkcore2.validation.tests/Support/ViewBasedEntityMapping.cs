using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class ViewBasedEntityMapping : IQueryTypeConfiguration<ViewBasedEntity>
    {
        public void Configure(QueryTypeBuilder<ViewBasedEntity> builder) {
            builder.ToView("ViewBasedEntities");
            builder.Property<int>("RoleId");
            builder.HasOne(entity => entity.TableEntity).WithOne().HasForeignKey<ViewBasedEntity>(entity => entity.Id).HasPrincipalKey<TableBasedEntity>(entity => entity.Id);
            builder.HasOne(entity => entity.Role).WithMany().HasForeignKey("RoleId");
        }
    }
}
