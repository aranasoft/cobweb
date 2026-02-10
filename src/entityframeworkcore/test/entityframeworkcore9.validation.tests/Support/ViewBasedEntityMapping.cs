using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support;
public class ViewBasedEntityMapping : IEntityTypeConfiguration<ViewBasedEntity> {
    public void Configure(EntityTypeBuilder<ViewBasedEntity> builder) {
        builder.ToView("ViewBasedEntities").ToTable((string?) null);
        builder.HasOne(entity => entity.TableEntity).WithMany().HasForeignKey(view => view.Id);
        builder.HasOne(entity => entity.Role).WithMany();
    }
}
