using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations;
public class ModuleConfiguration : EntityBaseConfiguration<Module>
{
    public override void Configure(EntityTypeBuilder<Module> builder)
    {
        base.Configure(builder);

        builder.Property(m => m.Name)
            .HasMaxLength(50);
    }
}
