using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support {
    public class ViewBasedEntityMapping : IQueryTypeConfiguration<ViewBasedEntity>
    {
        public void Configure(QueryTypeBuilder<ViewBasedEntity> builder) {
            builder.ToView("ViewBasedEntities");
        }
    }
}
