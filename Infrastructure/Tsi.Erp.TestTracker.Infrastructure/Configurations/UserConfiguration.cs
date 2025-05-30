using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // base.Configure(builder);

        // builder.Property(m => m.Name).HasMaxLength(50);

        //builder.Property(m => m.FirstName).HasMaxLength(25);

        //builder.Property(m => m.LastName).HasMaxLength(25);

        //builder.Property(m => m.Login).HasMaxLength(50);



        //builder.Property(m => m.Password).HasMaxLength(200);
        builder.HasData(new ApplicationUser()
        {
            Id = "a7467acf-4af4-4504-aa43-7ff02cad6d39",
            FirstName = "Louhichi",
            LastName = "Marwen",
            Login = "louhichi.marwen@outlook.com",
            UserName = "Louhichi",
            Surname = "Marwen",
            DisplayName = "Louhichi",
            Mail = "louhichi.marwen@outlook.com",
            Password = "",
            MailNickname = "admin",
            UserPrincipalName = "louhichi.marwen@testtracker.onmicrosoft.com",
            AccountEnabled = true,
            IsAdmin = true

        });

    }
}
