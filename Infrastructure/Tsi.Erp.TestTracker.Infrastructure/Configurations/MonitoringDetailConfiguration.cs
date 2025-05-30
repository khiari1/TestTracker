using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations;
public class MonitoringDetailConfiguration : EntityBaseConfiguration<MonitoringDetail>
{
    public override void Configure(EntityTypeBuilder<MonitoringDetail> builder)
    {
        base.Configure(builder);
        builder.Property(m => m.BuildVersion).HasMaxLength(50);
        builder.Property(m => m.Ticket).HasMaxLength(300);
        builder.Property(m => m.ExceptionType).HasMaxLength(300);
        builder.Property(m => m.Ticket).HasMaxLength(300);
    }
}
