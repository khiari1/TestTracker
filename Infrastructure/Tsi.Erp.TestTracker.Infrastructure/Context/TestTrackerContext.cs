using System.Linq.Extensions;
using Tsi.AspNetCore.Identity.EntityFrameworkCore;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Configurations;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Context;
public class TestTrackerContext : IdentityDbContext<ApplicationUser>
{
    public TestTrackerContext(DbContextOptions<TestTrackerContext> options) : base(options)
    {

    }

    #region Dbsets
    public virtual DbSet<Module> Modules { get; set; }
    public virtual DbSet<Monitoring> Monitorings { get; set; }
    public virtual DbSet<MonitoringDetail> MonitoringDetails { get; set; }
    public virtual DbSet<Attachement> Attachements { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Settings> Settings { get; set; }
    public virtual DbSet<ProjectFile> Assembly { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<QueryStore> QueryStores { get; set; }
    public virtual DbSet<Feature> Features { get; set; }
    public virtual DbSet<Label> Labels { get; set; }


    #endregion

    #region Entities Configurations
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

    }
    #endregion

}

