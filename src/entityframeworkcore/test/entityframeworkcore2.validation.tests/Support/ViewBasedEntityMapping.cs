using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class ViewBasedEntityMapping : IQueryTypeConfiguration<ViewBasedEntity> {
        public void Configure(QueryTypeBuilder<ViewBasedEntity> builder) {
            builder.ToView("ViewBasedEntities");
            builder.HasOne(entity => entity.TableEntity).WithMany().HasForeignKey(view => view.Id);
            builder.HasOne(entity => entity.Role).WithMany();
        }
    }
}
