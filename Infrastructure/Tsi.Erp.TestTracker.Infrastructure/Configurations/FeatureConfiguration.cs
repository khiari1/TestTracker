using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations
{
    public class FeatureConfiguration : EntityBaseConfiguration<Feature>
    {
        public override void Configure(EntityTypeBuilder<Feature> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Name).HasMaxLength(128);
            builder.Property(m => m.Description).HasMaxLength(512);

        }
    }
}
