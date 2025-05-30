using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.AccountEnabled).HasMaxLength(250);
            builder.Property(au => au.City).HasMaxLength(250);
            builder.Property(au => au.CompanyName).HasMaxLength(250);
            builder.Property(au => au.Country).HasMaxLength(250);
            builder.Property(au => au.Department).HasMaxLength(250);
            builder.Property(au => au.DisplayName).HasMaxLength(250);
            builder.Property(au => au.CreationType).HasMaxLength(128);
            builder.Property(au => au.EmployeeType).HasMaxLength(250);
            builder.Property(au => au.JobTitle).HasMaxLength(250);
            builder.Property(au => au.Mail).HasMaxLength(250);
            builder.Property(au => au.MobilePhone).HasMaxLength(250);
            builder.Property(au => au.OfficeLocation).HasMaxLength(250);
            builder.Property(au => au.PostalCode).HasMaxLength(250);
            builder.Property(au => au.StreetAddress).HasMaxLength(250);
            builder.Property(au => au.Surname).HasMaxLength(250);
        }
    }
}
