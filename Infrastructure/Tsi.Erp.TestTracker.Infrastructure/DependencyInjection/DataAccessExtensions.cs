using Tsi.AspNetCore.Identity.EntityFrameworkCore;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;
using Tsi.Extensions.Identity.Stores;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.DependencyInjection;
public static class DataAccessExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string defaultConnection)
    {
        services.AddDbContextFactory<TestTrackerContext>(option => option.UseSqlServer(defaultConnection));
        services.AddDbContext<TestTrackerContext>(option => option.UseSqlServer(defaultConnection));
        services.AddTsiIdentityStore(typeof(ApplicationUser), typeof(TestTrackerContext), typeof(TsiIdentityPermission));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IModuleRepository), typeof(ModuleRepository));
        services.AddScoped(typeof(IResourceRepository), typeof(ResourceRepository));
        services.AddScoped(typeof(IMonitoringRepository), typeof(MonitoringRepository));
        services.AddScoped(typeof(IFileRepository), typeof(FileRepository));
        services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
        services.AddScoped(typeof(JobRepository));
        services.AddScoped(typeof(IAssemblyRepository), typeof(AssemblyRepository));
        services.AddScoped(typeof(ISettingRepository), typeof(SettingsRepository));
        services.AddScoped(typeof(ILabelRepository), typeof(LabelRepository));

        return services;

    }
}
