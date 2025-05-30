using Microsoft.EntityFrameworkCore;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.AspNetCore.Identity.EntityFrameworkCore
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class IdentityDbContext : IdentityDbContext<TsiIdentityUser, TsiIdentityGroup, TsiIdentityPermission, string>
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    public abstract class IdentityDbContext<TUser> : IdentityDbContext<TUser, TsiIdentityGroup, TsiIdentityPermission, string>
     where TUser : TsiIdentityUser
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class IdentityDbContext<TUser, TKey> : IdentityDbContext<TUser, TsiIdentityGroup<TKey>, TsiIdentityPermission<TKey>, TKey>
         where TUser : TsiIdentityUser<TKey>
         where TKey : IEquatable<TKey>
    {

        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TGroup"></typeparam>
    /// <typeparam name="TPermission"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class IdentityDbContext<TUser, TGroup, TPermission, TKey> : DbContext
        where TUser : TsiIdentityUser<TKey>
        where TGroup : TsiIdentityGroup<TKey>
        where TPermission : TsiIdentityPermission<TKey>
        where TKey : IEquatable<TKey>

    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<TUser> TsiUsers { get; set; }
        public virtual DbSet<TGroup> TsiGroups { get; set; }
        public virtual DbSet<TPermission> TsiPermissions { get; set; }
        public virtual DbSet<TsiIdentityUserGroup<TKey>> TsiUserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TUser>(b =>
            {
                b.HasKey(a => a.Id);
                b.Property(m => m.UserName).HasMaxLength(128);
                b.Property(m => m.FirstName).HasMaxLength(128);
                b.Property(m => m.LastName).HasMaxLength(128);
                b.Property(m => m.Mail).HasMaxLength(128);
                b.Property(m => m.Password).HasMaxLength(128);
                b.Property(m => m.Login).HasMaxLength(128);
                b.Property(m => m.MobilePhone).HasMaxLength(128);
                b.HasMany<TsiIdentityUserGroup<TKey>>().WithOne().HasForeignKey(p => p.UserId).IsRequired();
            });

            builder.Entity<TGroup>(b =>
            {
                b.HasKey(a => a.Id);
                b.Property(m => m.Name).HasMaxLength(128);
                b.Property(m => m.Description).HasMaxLength(128);
                b.HasMany<TPermission>().WithOne().HasForeignKey(p => p.GroupId).IsRequired();
                b.HasMany<TsiIdentityUserGroup<TKey>>().WithOne().HasForeignKey(p => p.GroupId).IsRequired();
            });

            builder.Entity<TsiIdentityUserGroup<TKey>>(b =>
            {
                b.HasKey(a => new { a.UserId, a.GroupId });
            });

            builder.Entity<TPermission>(b =>
            {
                b.HasKey(a => a.Id);
                b.Property(m => m.Name).HasMaxLength(128);
                b.Property(m => m.Description).HasMaxLength(250);
                b.Property(m => m.Key).HasMaxLength(128);
                b.Property(m => m.Description).HasMaxLength(128);
            });
        }


    }
}
