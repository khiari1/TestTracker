using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations
{
    public class JobConfiguration : EntityBaseConfiguration<Job>
    {
        public override void Configure(EntityTypeBuilder<Job> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Name)
                .HasMaxLength(50);
        }
    }
}
