using Microsoft.EntityFrameworkCore.Migrations;
using System;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations;

public class MonitoringConfiguration : EntityBaseConfiguration<Monitoring>
{
    public override void Configure(EntityTypeBuilder<Monitoring> builder)
    {
        base.Configure(builder);

        builder.Property(m => m.NameMethodTest)
            .HasMaxLength(50);

        builder.HasOne(m => m.Tester)
            .WithMany()
            .HasForeignKey(m => m.TesterId);

        builder.HasOne(m => m.Responsible)
           .WithMany()
           .HasForeignKey(m => m.ResponsibleId);

        builder.Property(m => m.MonitoringTestType)
           .HasConversion(mtp => mtp.ToString(), 
                            mtp => (MonitoringTestType)Enum.Parse(typeof(MonitoringTestType), mtp))
           .IsUnicode(false);
    }
}
