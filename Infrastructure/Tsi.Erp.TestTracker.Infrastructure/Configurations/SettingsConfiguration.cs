using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations
{
    public class SettingsConfiguration : EntityBaseConfiguration<Settings>
    {
        public override void Configure(EntityTypeBuilder<Settings> builder)
        {
            base.Configure(builder);
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Key).HasMaxLength(250);
            builder.Property(s => s.Value);
        }
    }
}
