using Microsoft.Extensions.DependencyInjection;
using Tsi.Erp.TestTracker.Abstractions;
using Tsi.Erp.TestTracker.Core.Services;

namespace Tsi.Erp.TestTracker.Core
{
    public static class CoreExtensions
    {
            public static IServiceCollection AddCore(this IServiceCollection services)
            {

            services.AddScoped(typeof(JobManager));

            services.AddScoped(typeof(FileManager));

            services.AddScoped<SettingsService>();

            services.AddScoped<ProjectFileService>();

            

            return services;

        }

        public static IServiceCollection AddMailService(this IServiceCollection services,MailServiceOption mailServiceOption)
        {
            services.AddScoped(option =>
            {
                return new MailService(mailServiceOption.Host, mailServiceOption.Port, mailServiceOption.Mail, mailServiceOption.Password);
            });

            return services;
        }
        
    }
}
