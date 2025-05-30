using Microsoft.Extensions.DependencyInjection;
using Tsi.Erp.TestTracker.Abstractions;

namespace Tsi.Erp.TestTracker.TiketingSystem.AzureDevOps
{
    public static class AzureDevOpsExtensions
    {
        public static IServiceCollection AddTicketingSystem(this IServiceCollection services, string url,string token)
        {
            services.AddScoped<ITicketingSystem>(provider =>
            {
                return new AzureDevOpsService(url, token);
            });

            return services;

        }
    }
}
