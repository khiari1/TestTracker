using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Newtonsoft.Json;
using Hangfire.SqlServer;
using Tsi.Erp.TestTracker.Abstractions;


namespace Tsi.Erp.TestTracker.Hangfire
{
    public static class JobSystemExtensions
    {
        public static IServiceCollection AddJobSystem(this IServiceCollection services,string connectionString)
        {
            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true,
                    })
                    .UseFilter(new AutomaticRetryAttribute { Attempts = 1 })
                );

            services.AddHangfireServer();
            services.AddScoped(typeof(IJobSystem), typeof(HangfireJob));

            services.AddAutoMapper(typeof(MappingConfiguration));

            return services;

        }
    }
}
