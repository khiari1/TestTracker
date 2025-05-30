using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations
{
    public class LabelConfiguration : EntityBaseConfiguration<Label>
    {
        public override void Configure(EntityTypeBuilder<Label> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.Name).HasMaxLength(68);
            builder.Property(m => m.Description).HasMaxLength(128);
            builder.Property(m => m.Color).HasMaxLength(32);

        }
    }
}
